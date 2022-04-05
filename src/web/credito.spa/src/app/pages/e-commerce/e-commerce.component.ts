import { Component, TemplateRef } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { NbDialogRef, NbDialogService, NbIconLibraries } from "@nebular/theme";
import { LocalDataSource } from "ng2-smart-table";
import Swal from "sweetalert2";
import { Credito } from "../../@core/models/credito";
import { CreditoService } from "../../@core/services/credito.services";

@Component({
  selector: "ngx-ecommerce",
  templateUrl: "./e-commerce.component.html",
  styleUrls: ["./e-commerce.component.scss"],
})
export class ECommerceComponent {
  evaIcons = [];
  modal: NbDialogRef<any>;
  icon = "plus";
  iconMoney = "coins";
  source: LocalDataSource = new LocalDataSource();
  creditoForm: FormGroup;
  creditoEdit: Credito;

  settings = {
    mode: "external",
    editable: true,
    noDataMessage: "Dados não encontrados",
    hideSubHeader: true,
    actions: {
      columnTitle: "",
    },
    add: {
      addButtonContent: '<i class="nb-plus"></i>',
      createButtonContent: '<i class="nb-checkmark"></i>',
      cancelButtonContent: '<i class="nb-close"></i>',
    },
    edit: {
      editButtonContent: '<i class="nb-edit"></i>',
      saveButtonContent: '<i class="nb-checkmark"></i>',
      cancelButtonContent: '<i class="nb-close"></i>',
    },
    delete: {
      deleteButtonContent: '<i class="nb-trash"></i>',
      confirmDelete: true,
    },
    columns: {
      usuarioCod: {
        title: "Código do Usuário",
        type: "string",
      },
      usuarioNome: {
        title: "Nome do Usuário",
        type: "string",
      },
      cpf: {
        title: "CPF",
        type: "string",
      },
      dataNascimento: {
        title: "Data da Nascimento",
        type: "string",
      },
      salario: {
        title: "Salário",
        type: "string",
      },
      observacao: {
        title: "Observação",
        type: "string",
      },
    },
  };

  constructor(
    iconsLibrary: NbIconLibraries,
    private dialogService: NbDialogService,
    private formBuilder: FormBuilder,
    private creditoService: CreditoService
  ) {
    this.getCreditos();

    this.evaIcons = Array.from(iconsLibrary.getPack("eva").icons.keys()).filter(
      (icon) => icon.indexOf("outline") === -1
    );

    iconsLibrary.registerFontPack("fa", {
      packClass: "fa",
      iconClassPrefix: "fa",
    });
    iconsLibrary.registerFontPack("far", {
      packClass: "far",
      iconClassPrefix: "fa",
    });
    iconsLibrary.registerFontPack("ion", { iconClassPrefix: "ion" });

    this.creditoForm = this.formBuilder.group({
      id: [""],
      usuarioCod: [""],
      usuarioNome: [
        "",
        Validators.compose([Validators.required, Validators.min(10)]),
      ],
      cpf: [""],
      dataNascimento: [""],
      salario: [""],
      observacao: [""],
    });
  }

  ngOnInit(): void {}

  onDeleteCredito(event): void {
    let id = event.data.id;
    this.creditoService.deleteCredito(id).subscribe(
      (response) => {
        if (response.statusCode == 200) {
          Swal.fire({
            title: "Uhuuuul",
            text: response.message,
            icon: "success",
            focusConfirm: true,
          });
          this.getCreditos();
          this.modal.close();
        }
      },
      (error) => {
        Swal.fire({
          title: "Oops...",
          text: error["error"].errors.Messages[0],
          icon: "error",
          focusConfirm: true,
        });
      }
    );
  }

  onEditCredito(credito: Credito, dialog: TemplateRef<any>): void {
    this.creditoForm = this.formBuilder.group({
      id: [credito.id],
      usuarioCod: [credito.usuarioCod],
      usuarioNome: [credito.usuarioNome],
      cpf: [credito.cpf],
      dataNascimento: [credito.dataNascimento],
      salario: [credito.salario],
      observacao: [credito.observacao],
    });

    this.modal = this.dialogService.open(dialog, {
      context: {
        title: "Edit",
      },
      dialogClass: "modal-xl",
    });
  }

  updateCredito() {
    let id = this.creditoForm.get("id").value;
    let usuarioCod = this.creditoForm.get("usuarioCod").value;
    let usuarioNome = this.creditoForm.get("usuarioNome").value;
    let cpf = this.creditoForm.get("cpf").value;
    let dataNascimento = this.parseToStringDate(
      this.creditoForm.get("dataNascimento").value
    );
    let salario = this.creditoForm.get("salario").value;
    let observacao = this.creditoForm.get("observacao").value;

    let credito = new Credito(
      id,
      usuarioCod,
      usuarioNome,
      cpf,
      dataNascimento,
      salario,
      observacao
    );

    this.creditoService.updateCredito(credito).subscribe(
      (response) => {
        if (response.statusCode == 200) {
          Swal.fire({
            title: "Uhuuuul",
            text: "Crédito atualizado com sucesso",
            icon: "success",
            focusConfirm: true,
          });
          this.getCreditos();
          this.modal.close();
        }
      },
      (error) => {
        Swal.fire({
          title: "Oops...",
          text: error["error"].errors.Messages[0],
          icon: "error",
          focusConfirm: true,
        });
      }
    );
  }

  addCredito(dialog: TemplateRef<any>): void {
    this.creditoForm.reset();
    this.modal = this.dialogService.open(dialog, {
      context: {
        title: "Add",
      },
      dialogClass: "modal-xl",
    });
  }

  cancelCredito(dialog: TemplateRef<any>): void {
    this.modal.close();
  }

  saveCredito() {
    let usuarioCod = this.creditoForm.get("usuarioCod").value;
    let usuarioNome = this.creditoForm.get("usuarioNome").value;
    let cpf = this.creditoForm.get("cpf").value;
    let dataNascimento = this.parseToStringDate(
      this.creditoForm.get("dataNascimento").value
    );
    let salario = this.creditoForm.get("salario").value;
    let observacao = this.creditoForm.get("observacao").value;

    let credito = new Credito(
      "",
      usuarioCod,
      usuarioNome,
      cpf,
      dataNascimento,
      salario,
      observacao
    );

    var errors = credito.isValid();
    if (errors.length > 0) {
      Swal.fire({
        title: "Oops...",
        text: errors[0].message,
        icon: "error",
        focusConfirm: true,
      });
      return;
    }

    this.creditoService.createCredito(credito).subscribe(
      (response) => {
        if (response.statusCode == 200) {
          Swal.fire({
            title: "Uhuuuul",
            text: "Crédito cadastrada com sucesso",
            icon: "success",
            focusConfirm: true,
          });
          this.getCreditos();
          this.modal.close();
        }
      },
      (error) => {
        Swal.fire({
          title: "Oops...",
          text: error["error"].errors.Messages[0],
          icon: "error",
          focusConfirm: true,
        });
      }
    );
  }

  parseToStringDate(date: string): string {
    let dateSplit = date.split("/").join("");
    var dateFormated =
      dateSplit.substring(4, 8) +
      "-" +
      dateSplit.substring(2, 4) +
      "-" +
      dateSplit.substring(0, 2);
    return dateFormated;
  }

  getCreditos() {
    this.creditoService.getCreditos().subscribe((data) => {
      data.map(
        (credito) => (credito.cpf = this.formataCPF(credito.cpf.number))
      );
      data.map(
        (credito) =>
          (credito.dataNascimento = new Intl.DateTimeFormat("pt-BR").format(
            new Date(credito.dataNascimento)
          ))
      );
      this.source.load(data);
    });
  }

  formataCPF(cpf) {
    cpf = cpf.replace(/[^\d]/g, "");
    return cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4");
  }
}
