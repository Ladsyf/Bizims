// angular import
import { Component, inject } from '@angular/core';
import { SharedModule } from '../../../../shared/shared.module';
import { LayoutService } from '../../../services/layout.service';

@Component({
  selector: 'app-nav-left',
  imports: [SharedModule],
  templateUrl: './toolbar-left.component.html',
  styleUrls: ['./toolbar-left.component.scss']
})
export class NavLeftComponent {
  private layoutService = inject(LayoutService);

  // public method
  toggleMenu() {
    this.layoutService.toggleSideDrawer();
  }
}
