export interface Password {
  id?: number;
  category: string;
  app: string;
  userName: string;
  password: string;
  encryptedPassword: string;
  decryptedPassword: string;
}