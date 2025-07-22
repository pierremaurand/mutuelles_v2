import { SearchService } from './../../../core/services/search.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, ReactiveFormsModule } from '@angular/forms';
import { map, Observable, startWith, tap } from 'rxjs';

@Component({
  selector: 'app-header',
  imports: [ReactiveFormsModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements OnInit {
  searchCtrl!: FormControl;
  search$!: Observable<string>;
  dateCtrl!: FormControl;
  date$!: Observable<string>;

  constructor(private searchService: SearchService, private fb: FormBuilder) {}

  ngOnInit(): void {
    this.initControls();
    this.initObservables();
  }

  private initObservables() {
    this.search$ = this.searchCtrl.valueChanges.pipe(
      startWith(this.searchCtrl.value),
      map((value) => value.toLowerCase())
    );

    this.search$.subscribe({
      next: (value: string) => {
        this.searchService.setSearch(value);
      },
    });

    this.date$ = this.dateCtrl.valueChanges.pipe(
      startWith(this.dateCtrl.value)
    );

    this.date$.subscribe({
      next: (value: string) => {
        this.searchService.setDate(value);
      },
    });
  }

  private initControls(): void {
    this.searchCtrl = this.fb.control('');
    this.dateCtrl = this.fb.control('');
  }
}
