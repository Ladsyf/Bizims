import { BreakpointObserver } from '@angular/cdk/layout';
import { Component, OnInit, inject, viewChild } from '@angular/core';
import { MatDrawer, MatDrawerMode } from '@angular/material/sidenav';
import { RouterModule } from '@angular/router';
import { environment } from '../../../environments/environment.prod';
import { BreadcrumbComponent } from '../../@theme/layouts/breadcrumb/breadcrumb.component';
import { FooterComponent } from '../../@theme/layouts/footer/footer.component';
import { VerticalMenuComponent } from '../../@theme/layouts/menu/vertical-menu';
import { NavBarComponent } from '../../@theme/layouts/toolbar/toolbar.component';
import { LayoutService } from '../../@theme/services/layout.service';
import { menus } from '../../data/menu';
import { SharedModule } from '../../shared/shared.module';

@Component({
  selector: 'app-admin',
  imports: [FooterComponent, BreadcrumbComponent, SharedModule, RouterModule, NavBarComponent, VerticalMenuComponent],
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {
  private breakpointObserver = inject(BreakpointObserver);
  private layoutService = inject(LayoutService);

  // public props
  sidebar = viewChild<MatDrawer>('sidebar');
  menus = menus;
  modeValue: MatDrawerMode = 'side';
  currentApplicationVersion = environment.appVersion;

  // life cycle event
  ngOnInit() {
    this.breakpointObserver.observe(['(min-width: 1025px)', '(max-width: 1024.98px)']).subscribe((result) => {
      if (result.breakpoints['(max-width: 1024.98px)']) {
        this.modeValue = 'over';
      } else if (result.breakpoints['(min-width: 1025px)']) {
        this.modeValue = 'side';
      }
    });

    this.layoutService.layoutState.subscribe(() => {
      this.sidebar()?.toggle();
    });
  }
}
