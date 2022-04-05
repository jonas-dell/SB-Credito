export class Credito {
  id: string;
  usuarioCod: number;
  usuarioNome: string;
  cpf: string;
  dataNascimento: string;
  salario: number;
  observacao: string;

  constructor(
    id: string = "",
    usuarioCod: number,
    usuarioNome: string,
    cpf: string,
    dataNascimento: string,
    salario: number,
    observacao: string
  ) {
    this.id = id;
    this.usuarioCod = usuarioCod;
    this.usuarioNome = usuarioNome;
    this.cpf = cpf;
    this.dataNascimento = dataNascimento;
    this.salario = salario;
    this.observacao = observacao;
  }

  isValid(): Array<Error> {
    var errors = new Array<Error>();

    if (!this.usuarioNome)
      errors.push(new Error("O nome do cliente deve ser preenchido"));

    if (!this.cpf) errors.push(new Error("O cpf deve ser preenchido"));

    if (!this.dataNascimento)
      errors.push(new Error("A data de nascimento deve ser preenchido"));

    if (!this.isValidDate(this.dataNascimento))
      errors.push(
        new Error("A data da movimentação está em um formato inválido")
      );

    if (!this.observacao)
      errors.push(new Error("O código do tributo deve ser preenchido"));

    return errors;
  }

  isValidDate(data: string): boolean {
    return new Date(data).toUTCString() !== "Invalid Date";
  }
}
