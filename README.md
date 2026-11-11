# manage-affected-bookings
## Anton Carlfors Wennbom

Instruktioner för att köra programmet.
* Klona repositoryt, Solution inehåller endast ett projekt, en console-applilkation. Det är den som ska vara startup projekt, ManageAffectedBookings.Application.
* Kör applikationen och följ instruktionerna.

## Huvudflöde och Alternativa flöden

* Välj användare, giltiga val (1,2,3).
* Välj utrustning för körningen, det står listat hur många berörda bokningar de olika utrustningarna har.
* Om du valde Customer avslutas flödet här då denne inte har behörighet att ändra status. 
* Nu ska den valda utrustningen sättas till otillgänglig, ange en orsak, (måste vara ifylld).
* Nu listas berörda bokningar om det fanns på utrustningen samt att den valda uppdaterade utrustningen skrivs ut.
Om ingen berörd bokning finns visas meddelande om det.
