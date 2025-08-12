import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { EcheanceService } from '../../../../core/services/echeance.service';
import { Echeance } from '../../../../core/models/echeance';
import { combineLatest, map, Observable, startWith } from 'rxjs';
import { AsyncPipe, CommonModule } from '@angular/common';
import {
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { MouvementService } from '../../../../core/services/mouvement.service';
import { LigneEcheance } from '../../../composants/ligne-echeance/ligne-echeance';

@Component({
  selector: 'app-paiement',
  imports: [CommonModule, AsyncPipe, ReactiveFormsModule, LigneEcheance],
  templateUrl: './paiement.html',
  styleUrl: './paiement.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class Paiement implements OnInit {
  echeancier$!: Observable<Echeance[]>;
  request!: FormGroup;
  datePaiementCtrl!: FormControl;
  items: any[] = [];

  constructor(
    private echeanceService: EcheanceService,
    private mouvementService: MouvementService,
    private fb: FormBuilder,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.initObservables();
  }

  initForm(): void {
    this.datePaiementCtrl = this.fb.control('', Validators.required);
    this.request = this.fb.group({
      lines: this.fb.array([]),
    });
  }

  get lines(): FormArray {
    return this.request.get('lines') as FormArray;
  }

  initObservables(): void {
    const date$ = this.datePaiementCtrl.valueChanges.pipe(
      startWith(this.datePaiementCtrl.value),
      map((value) => value.substr(0, 7))
    );

    this.echeancier$ = combineLatest([
      date$,
      this.echeanceService.echeances$,
    ]).pipe(
      map(([d, echeances]) =>
        echeances.filter(
          (e) => e.montantRestant > 0 && e.dateEcheance.includes(d)
        )
      )
    );

    this.echeancier$.subscribe((echeances) => {
      this.lines.clear();
      echeances.forEach(() => this.items.push({ checked: false }));
    });
  }

  submitForm(): void {
    if (this.request.valid && this.lines.length > 0) {
      this.mouvementService
        .remboursement(this.lines.value as Echeance[])
        .subscribe({
          next: () => {
            this.toastr.success("L'enregistrement a réussie!", 'Succès');
            this.onCancel();
          },
          error: () => {
            this.toastr.error('Une erreur est survenue!', 'Erreur');
          },
        });
    } else {
      this.toastr.error('Vous devez cocher au moins une échéance.', 'Erreur');
    }
  }

  onCancel(): void {
    this.router.navigateByUrl('/caisse');
  }

  onCheck(i: number, echeance: Echeance, $event: any) {
    this.items[i].checked = $event.target.checked;
    if ($event.target.checked) {
      this.addLine(echeance);
    } else {
      this.removeLine(echeance);
    }
  }

  addLine(echeance: Echeance): void {
    const existingIndex = this.lines.controls.findIndex(
      (line) => line.value.id === echeance.id
    );
    if (existingIndex === -1) {
      const echeanceForm = this.fb.group({
        id: [echeance.id, Validators.required],
        avanceId: [echeance.avanceId, Validators.required],
        dateEcheance: [echeance.dateEcheance, Validators.required],
        montantCapital: [echeance.montantCapital, Validators.required],
        montantInterets: [echeance.montantInterets, Validators.required],
        datePaiement: [echeance.dateEcheance, Validators.required],
      });
      this.lines.push(echeanceForm);
    }
  }

  removeLine(echeance: Echeance): void {
    const index = this.lines.controls.findIndex(
      (line) => line.value.id === echeance.id
    );
    if (index !== -1) {
      this.lines.removeAt(index);
    }
  }

  onCheckAll(echeancier: Echeance[], $event: any) {
    if ($event.target.checked) {
      echeancier.forEach((echeance) => {
        this.addLine(echeance);
      });
      this.items = this.items.map(() => ({ checked: true }));
    } else {
      echeancier.forEach((echeance) => {
        this.removeLine(echeance);
      });
      this.items = this.items.map(() => ({ checked: false }));
    }
  }
}
