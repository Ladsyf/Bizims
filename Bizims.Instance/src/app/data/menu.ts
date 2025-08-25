import { Navigation } from "../@theme/types/navigation";
import { newGuid } from "../global-helper";

export const menus: Navigation[] = [
  {
    id: newGuid(),
    title: 'Dashboard',
    type: 'item',
    classes: 'nav-item',
    url: '/dashboard/product-listing',
    icon: '#custom-status-up',
  },
  {
    id: newGuid(),
    title: 'Inventory',
    type: 'group',
    children: [
      {
        id: newGuid(),
        title: "Stocks Management",
        type: "item",
        classes: "nav-item",
        url: '/inventory/stocks',
        icon: '#custom-text-block'
      },
      {
        id: newGuid(),
        title: 'Suppliers',
        type: 'item',
        classes: 'nav-item',
        url: '/inventory/supplier',
        icon: '#custom-text-block'
      },
      {
        id: newGuid(),
        title: 'Purchase Orders',
        type: 'item',
        classes: 'nav-item',
        url: '/inventory/orders',
        icon: '#custom-text-block'
      },
    ]
  },
  {
    id: newGuid(),
    title: "Sales & Customers",
    type: "group",
    children: [
      {
        id: newGuid(),
        title: "Customers",
        type: 'item',
        classes: 'nav-item',
        url: '/crm/customers',
        icon: '#custom-text-block'
      },
      {
        id: newGuid(),
        title: "Sales Orders",
        type: 'item',
        classes: 'nav-item',
        url: '/crm/orders',
        icon: '#custom-text-block'
      }, // merge invoices
    ]
  },
  {
    id: newGuid(),
    title: "Finances",
    type: 'group',
    children: [
      {
        id: newGuid(),
        title: "Expenses",
        type: 'item',
        classes: 'nav-item',
        url: '/finances/expenses',
        icon: '#custom-text-block'
      },
      {
        id: newGuid(),
        title: "Budgets",
        type: 'item',
        classes: 'nav-item',
        url: '/finances/budgets',
        icon: '#custom-text-block'
      },
      {
        id: newGuid(),
        title: "Reports",
        type: 'item',
        classes: 'nav-item',
        url: '/finances/sales-report',
        icon: '#custom-text-block'
      },
      // {
      //   id: newGuid(),
      //   title: "Recurring Bills",
      //   type: 'item',
      //   classes: 'nav-item',
      //   url: '/finances/bills',
      //   icon: '#custom-text-block'
      // }
    ]
  },
  {
    id: newGuid(),
    title: "Team",
    type: "group",
    children: [
      {
        id: newGuid(),
        title: "Employees",
        type: "item",
        classes: "nav-item",
        url: "/employees/employees",
        icon: '#custom-text-block'
      },
      {
        id: newGuid(),
        title: "Payroll",
        type: "item",
        classes: "nav-item",
        url: "/employees/payroll",
        icon: '#custom-text-block'
      },
    ]
  }
];
