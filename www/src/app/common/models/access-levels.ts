export type AccessLevel =
  | 'Guest'
  | 'Employee'
  | 'TechnicalEmployee'
  | 'Administrator'
  | 'God'
  | 'Blocked';

export const accessLevels = [
  'Guest',
  'Employee',
  'TechnicalEmployee',
  'Administrator',
  'God',
  'Blocked',
];
