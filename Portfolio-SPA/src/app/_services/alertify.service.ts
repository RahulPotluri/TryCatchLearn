import { Injectable } from '@angular/core';
/// For ts lint, let it know that we are going to use it
declare let alertify: any;

@Injectable({
  providedIn: 'root'
})

/// Wrapping external library into a service, can also be used normally
export class AlertifyService {

constructor() { }

confirm(message: string, okCallback: () => any) {
 alertify.confirm(message, function(e) {
   if (e) {
     okCallback();
   } else {}

 });
}

success(message: string) {
  alertify.success(message);
}

error(message: string) {
  alertify.error(message);
}

warning(message: string) {
  alertify.warning(message);
}

message(message: string) {
  alertify.message(message);
}

}
