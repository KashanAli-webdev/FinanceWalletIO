import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ToasterService {
  toast = {
    active: false,
    msg: '',
    res: ''
  }

  TriggerNotify(msg: string, res: string): void {
    this.toast.active = true;
    this.toast.msg = msg;
    this.toast.res = res;

    // reset after 3 seconds.
    setTimeout(() => {
      this.toast.active = false;
      this.toast.msg = '';
      this.toast.res = '';
    }, 4000);
  }

}
