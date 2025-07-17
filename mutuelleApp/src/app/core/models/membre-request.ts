import { Sexe } from './sexe';

export class MembreRequest {
  nom!: string;
  sexe!: Sexe;
  dateNaissance!: string;
  lieuNaissance!: string;
  agenceId!: number;
  dateAdhesion!: string;
  telephone!: string;
  email!: string;
  photo?: string;
  estActif: boolean = true;
}
