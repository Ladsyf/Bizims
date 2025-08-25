import { Navigation } from "../@theme/types/navigation";
import { newGuid } from "../global-helper";

export const menus: Navigation[] = [
  {
    id: newGuid(),
    title: 'Businesses',
    type: 'item',
    classes: 'nav-item',
    url: '/',
    icon: '#custom-status-up',
  },
];
