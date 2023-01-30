import { User } from './user';

export class UserParams {
  pageNumber = 1;
  pageSize = 5;
  //orderBy = 'lastActive';
  orderBy = 'userName';

  constructor(user: User | null) {}
}
