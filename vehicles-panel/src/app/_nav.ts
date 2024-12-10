import { INavData } from '@coreui/angular';


export const navItems: INavData[] = [

  // {
  //   name: 'Dashboard',
  //   url: '/dashboard',
  //   icon: 'icon-speedometer',
  //   attributes: { disabled: true },
  // },
  // {
  //   name: 'Admin Management',
  //   url: '/dashboard',
  //   icon: 'cui-dashboard ',
  //   attributes: { disabled: true },
  // },
  // {
  //   title: true,
  //   name: 'Site Management'
  // },
  {
    name: 'Dashboard ',
    url: '/dashboard',
    icon: 'cui-dashboard',
  },
  {
    name: 'Committee Management',
    url: '/committee-management',
    icon: 'fa fa-user',
  },
  // {
  //   name: 'RFQ ',
  //   url: '/auth',
  //   icon: 'fa fa-circle nav-icon',
  //   attributes: { disabled: true },
  // },
  {
    name: 'Registration Requests ',
    url: '/registrationManagement',
    icon: 'fa fa-user',
  },
  {
    name: 'User Management ',
    url: '/userManagement',
    icon: 'fa fa-user',
  },
  {
    name: 'Service End-Points ',
    url: '/ws-endpoints',
    icon: 'fa fa-user',
  },
  {
    name: 'Tenders ',
    url: '/auth',
    icon: 'fa fa-circle nav-icon',
    children: [{
      name: 'Tenders',
      url: '/auth',
      icon: 'fa fa-circle nav-icon',
      attributes: { disabled: true },
    },
    {
      name: 'Supplier',
      url: '/auth',
      icon: 'fa fa-circle nav-icon',
      attributes: { disabled: true },
    },
    {
      name: 'Supplier Management',
      url: '/auth',
      icon: 'fa fa-circle nav-icon',
      attributes: { disabled: true },
    }
    ]
  },
  {
    name: 'Settings ',
    url: '/auth',
    icon: 'icon-speedometer',
    attributes: { disabled: true },
  },

  {
    name: 'Logout ',
    url: '/auth',
    icon: 'fa fa-sign-out',
  },
];
