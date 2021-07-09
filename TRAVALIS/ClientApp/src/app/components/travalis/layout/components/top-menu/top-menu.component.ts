import { Component, Output, EventEmitter, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'top-menu',
  templateUrl: './top-menu.component.html',
  styleUrls: ['./top-menu.component.css']

})
export class TopMenuComponent {

  @Input() sidenav;

  constructor(
    private _router: Router
  ) { }

  public openNotifications() { }

  public logOut() {
    // this._utilitiesService.logOut();
    this._router.navigate(['/home/login']);
  }

  public getEnvironment() { 'Development' }


}
