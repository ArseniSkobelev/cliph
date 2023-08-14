// place files you want to import through the `$lib` alias in this folder.
export type Response = {
  success: boolean;
  message: string;
  data: any | undefined;
};

export type JwtResponse = {
  jwt: string;
};

export type ApiKey = {
  key: string | undefined;
};