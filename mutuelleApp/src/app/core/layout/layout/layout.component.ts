import { UtilisateurService } from './../../services/utilisateur.service';
import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from '../../../ui/composants/header/header.component';
import { SideBarComponent } from '../../../ui/composants/side-bar/side-bar.component';

@Component({
  selector: 'app-layout',
  imports: [RouterOutlet, HeaderComponent, SideBarComponent],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.scss',
})
export default class LayoutComponent implements OnInit {
  constructor(private utilisateurService: UtilisateurService) {}
  ngOnInit(): void {
    this.utilisateurService.getAllUtilisateurFromServer();
  }
}
