import { ErrorType } from '../../enums/error-type';

export interface ApiResponse {
  data?: any;
  errors?: string[];
  errorType?: ErrorType;
  succeeded?: boolean;
}
