import { Role } from './role';
import { Sexe } from './sexe';

export class UpdateUtilisateurRequest {
  login!: string;
  nom!: string;
  sexe?: Sexe;
  role?: Role;
}
