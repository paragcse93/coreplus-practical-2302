export interface Practitioner {
  id: number;
  name: string;
  level: PractitionerLevel;
}

enum PractitionerLevel {
  OWNER,
  ADMIN,
  CASE_MANAGER,
  PRACTITIONER,
}
