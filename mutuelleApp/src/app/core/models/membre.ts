import { Sexe } from './sexe';

export class Membre {
  id: number = 0;
  nom: string = '';
  sexe: Sexe = Sexe.Masculin;
  nomSexe: string = '';
  dateNaissance: string = '';
  lieuNaissance: string = '';
  agenceId: number = 0;
  nomAgence: string = '';
  dateAdhesion: string = '';
  telephone: string = '';
  email: string = '';
  photo: string = '';
  estActif: boolean = true;
  solde: number = 0;
  montantCotise: number = 0;
}
