import { Component } from '@angular/core';
import { OutTransactCreate } from "../out-transact-create/out-transact-create";
import { OutTransactDetails } from "../out-transact-details/out-transact-details";
import { OutTransactUpdate } from "../out-transact-update/out-transact-update";

@Component({
  selector: 'app-out-transact-home',
  imports: [OutTransactCreate, OutTransactDetails, OutTransactUpdate],
  templateUrl: './out-transact-home.html',
  styleUrl: './out-transact-home.css'
})
export class OutTransactHome {

}
