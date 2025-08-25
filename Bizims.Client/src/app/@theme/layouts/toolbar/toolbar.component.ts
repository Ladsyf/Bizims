import { Component } from '@angular/core';
import { NavLeftComponent } from './toolbar-left/toolbar-left.component';
import { NavRightComponent } from './toolbar-right/toolbar-right.component';
import { SharedModule } from '../../../shared/shared.module';

@Component({
  selector: 'app-nav-bar',
  imports: [SharedModule, NavLeftComponent, NavRightComponent],
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class NavBarComponent {}
