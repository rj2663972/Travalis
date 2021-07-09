import { UtilitiesService } from 'src/app/services/utilities/utilities.service';
import { Component, OnInit, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'left-menu',
  templateUrl: './left-menu.component.html',
  styleUrls: ['./left-menu.component.css']
})
export class LeftMenuComponent implements OnInit, OnDestroy {

  showSaleOptions = false;

  constructor(
    public router: Router
  ) {

  }

  ngOnInit() {

  }

  ngOnDestroy() {

  }

  public logOut() {
    // this._utilitiesService.logOut();
    this.router.navigate(['/home/login']);
  }

  expandSalesOptions() {
    this.showSaleOptions = !this.showSaleOptions;
  }
}
