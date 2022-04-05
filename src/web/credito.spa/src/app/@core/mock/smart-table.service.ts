import { Injectable } from "@angular/core";
import { SmartTableData } from "../data/smart-table";

@Injectable()
export class SmartTableService extends SmartTableData {
  data = [
    {
      id: 1,
      customerName: "Jonas Cardoso",
      cpf: "947.299.650-78",
      movimentationDate: "01/03/2020",
      tributeCode: "001",
      tributeDescription: "Teste",
      tributeAliquot: "1000",
      movimentationGain: "100000",
      movimentationLoss: "1500",
    },
    {
      id: 2,
      customerName: "Pâmela Cardoso",
      cpf: "782.966.570-23",
      movimentationDate: "01/10/2020",
      tributeCode: "002",
      tributeDescription: "Teste",
      tributeAliquot: "1000",
      movimentationGain: "100000",
      movimentationLoss: "1500",
    },
  ];

  getData() {
    return this.data;
  }
}
