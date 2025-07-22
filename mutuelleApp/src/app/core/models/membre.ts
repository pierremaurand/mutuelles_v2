import { Sexe } from './sexe';

export class Membre {
  id: number = 0;
  nom: string = '';
  sexe: Sexe = Sexe.Masculin;
  dateNaissance: string = '';
  lieuNaissance: string = '';
  agenceId: number = 0;
  dateAdhesion: string = '';
  telephone: string = '';
  email: string = '';
  photo: string = '';
  estActif: boolean = true;
}
