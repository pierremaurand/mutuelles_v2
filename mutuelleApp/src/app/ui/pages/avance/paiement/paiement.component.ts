import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { AvanceService } from '../../../../core/services/avance.service';
import { EcheanceService } from '../../../../core/services/echeance.service';
import { Echeance } from '../../../../core/models/echeance';
import { combineLatest, map, Observable } from 'rxjs';
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
import { Avance } from '../../../../core/models/avance';

@Component({
  selector: 'app-paiement',
  imports: [CommonModule, AsyncPipe, ReactiveFormsModule],
  templateUrl: './paiement.component.html',
  styleUrl: './paiement.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class PaiementComponent implements OnInit {
  id: number = 0;
  avance$!: Observable<Avance>;
  echeancier$!: Observable<Echeance[]>;
  request!: FormGroup;
  datePaiementCtrl!: FormControl;
  items: any[] = [];

  constructor(
    private avanceService: AvanceService,
    private echeanceService: EcheanceService,
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
    this.avance$ = this.avanceService.avance$;
    this.avance$.subscribe((avance) => {
      this.id = avance.id;
    });
    this.echeancier$ = combineLatest([
      this.avanceService.avance$,
      this.echeanceService.echeances$,
    ]).pipe(
      map(([avance, echeances]) => {
        return echeances.filter(
          (e) => e.avanceId === avance.id && e.montantRestant > 0
        );
      })
    );
    this.echeancier$.subscribe((echeances) => {
      this.lines.clear();
      echeances.forEach(() => this.items.push({ checked: false }));
    });
  }

  submitForm(): void {
    if (this.request.valid && this.lines.length > 0) {
      this.avanceService
        .anticipationPaiement(this.id, this.lines.value as Echeance[])
        .subscribe({
          next: () => {
            this.toastr.success("L'enregistrement a réussie!", 'Succès');
            this.onCancel();
          },
          error: (error) => {
            console.log(error);
            if (error.status === 400) {
              this.toastr.error(error.error, 'Erreur de validation');
            } else {
              this.toastr.error('Une erreur est survenue!', 'Erreur');
            }
          },
        });
    } else {
      this.toastr.error('Vous devez cocher au moins une échéance.', 'Erreur');
    }
  }

  onCancel(): void {
    this.router.navigateByUrl('/avance/view/' + this.id);
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
        datePaiement: this.datePaiementCtrl,
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
