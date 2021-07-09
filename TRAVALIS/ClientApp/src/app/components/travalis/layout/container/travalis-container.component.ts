import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';


@Component({
  selector: 'app-container',
  templateUrl: './travalis-container.component.html',
  styleUrls: ['./travalis-container.component.css']
})

export class TravalisContainerComponent implements OnInit {
	sidebarMode = 'side';

  constructor(private _router: Router,
  ) {

  }

  ngOnInit() {

  }

}
