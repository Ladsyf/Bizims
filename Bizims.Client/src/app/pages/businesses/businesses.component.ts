import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';

@Component({
  templateUrl: './businesses.component.html',
  imports: [SharedModule, RouterModule],
})
export class BusinessesComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
