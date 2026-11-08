## Anton Carlfors Wennbom - Itteration 1

### Sekvensdiagram v1  
F-15 - Utrustning - Visa berörda bokningar  
US-25 - Som personal vill jag se vilka framtida bokningar som berörs så att jag kan agera i tid.

### Aktivitetsdiagram 24

```plantuml
@startuml

start

:Personal väljer exemplar;
:Anger orsak;

if (Orsak angiven?) then (Ja)

    if (Har personalbehörighet?) then (Ja)

        :Markera exemplar som otillgängligt;

        note right
        Befintliga bokningar
        avbokas inte
        automatiskt (VR10)
        end note

        :Spara status och orsak;

    else (Nej)

        :Visa behörighetsfel;

    endif

else (Nej)

    :Visa "Orsak måste anges";

endif

stop

@enduml

```

![Aktivitetsdiagram 24](Images/AD-24.png)

### Aktivitetsdiagram 24-25


```plantuml
@startuml

start

:Personal markerar exemplar som otillgängligt;

if (Har personalbehörighet?) then (Ja)

    :Spara status som otillgänglig;

    note right
    VR10:
    Befintliga bokningar
    avbokas inte automatiskt
    end note

    :Hämta framtida bokningar;

    :Filtrera bort ej aktiva bokningar;

    if (Finns berörda bokningar?) then (Ja)

        :Visa lista med berörda bokningar;

        note right
        Bokningsnummer
        Kundnamn
        E-post
        Telefon
        Period
        end note

    else (Nej)

        :Visa "Inga bokningar berörs";

    endif

else (Nej)

    :Visa behörighetsfel;

endif

stop

@enduml
```

![Aktivitetsdiagram 24-24](Images/AD-24-25.png)

### Sekvensdiagram SD-24

```plantuml

@startuml

actor Personal

participant UI
participant API
database Database

Personal -> UI : Markera exemplar som otillgängligt
Personal -> UI : Ange orsak

UI -> API : MarkeraOtillgänglig(exemplarId, orsak)

alt Orsak saknas

    API --> UI : Valideringsfel
    UI --> Personal : Orsak måste anges

else Orsak finns

    API -> API : Kontrollera personalbehörighet

    alt Behörighet saknas

        API --> UI : 403 Forbidden
        UI --> Personal : Åtkomst nekad

    else Behörighet OK

        API -> Database : Spara status och orsak

        Database --> API : OK

        note over API
        VR10:
        Befintliga bokningar
        avbokas inte automatiskt
        end note

        API --> UI : Exemplar uppdaterat

        UI --> Personal : Status sparad

    end

end

@enduml

```

![Sekvensdiagram 24](Images/SD-24.png)

### Sekvensdiagram SD-24-25

```plantuml
@startuml

actor Personal

participant UI
participant API
participant BookingService
database Database

Personal -> UI : Markera exemplar som otillgängligt

UI -> API : Markera exemplar som otillgängligt

API -> API : Kontrollera personalbehörighet

alt Behörighet saknas

    API --> UI : Behörighetsfel

    UI --> Personal : Åtkomst nekad

else Behörighet OK

    API -> Database : Spara status = Otillgänglig

    Database --> API : OK

    note over API
    VR10:
    Befintliga bokningar
    avbokas inte automatiskt
    end note

    API -> BookingService : Hämta framtida bokningar(exemplarId)

    BookingService -> Database : Hämta framtida bokningar

    Database --> BookingService : Bokningslista

    BookingService -> BookingService : Filtrera bort ej aktiva bokningar

    alt Bokningar finns

        BookingService --> API : Berörda bokningar

        API --> UI : Berörda bokningar

        UI --> Personal : Visa lista med berörda bokningar

        note right
        Bokningsnummer
        Kundnamn
        E-post
        Telefon
        Period
        end note

    else Inga bokningar finns

        BookingService --> API : Tom lista

        API --> UI : Tom lista

        UI --> Personal : Inga bokningar berörs

    end

end

@enduml
```
![Sekvensdiagram 24-25](Images/SD-24-25.png)

### Insikter

Det finns en task med att kolla berhörigheten men inte i AC för denna User Story. Men det bör kanske läggas till

### AC5

Given att användaren inte har personalbehörighet  
When användaren försöker visa berörda bokningar  
Then ska åtkomst nekas

---

### Samt i: US-24 - Som personal vill jag markera ett exemplar som otillgängligt så att ingen bokar trasig utrustning.
### AC

Given att användaren inte har personalbehörighet  
When användaren försöker markera ett exemplar som otillgängligt  
Then ska åtgärden nekas

---
## Egna beslut om förändringar i itteration 1

Ändring i Task 118 från: **Backend - Hämta framtida bokningar för exemplar**  
till: **Backend - Hämta alla bokningar där Bokningens slutdatum >= dagens datum**

När jag skulle börja med implementationen av US-25 så tänkte jag att det kanske är lättre att även ta med US-24 

---
### Att göra för implementation S-24:
* Skapa enum ToolType example. (Drill, Digger, Jigsaw, Sander)
* Skapa enum ExemplarStatus (Available, Unavailable)
* Skapa modell för Utrustning (guid Id, string name, string, Description, ToolType toolType)
* Skapa modell för Exemplar (guid id, guid UtrustningsId, ExemplarStatus Status, string? UnavailableReason) 
* Skapa enum Role (customer, employee, admin)
* Skapa modell för Inloggad (guid Id, string Kundnamn, string Email, string Phone, Role role)
* Skapa enum BookingStatus (Confirmed, Cancelled, Completed)
* Skapa modell för BookingPeriod (DateOnly start, DateOnly end)
* Skapa modell för bokning (guid Id, Exemplar exemplar, guid KundId, BookingStatus Status, BookingPeriod bookinngPeriod)
* Skapa Record UnavailableRequest för att markera utrustning otillgänglig (guid id, string reason)
* Validera att Request är korrekt ifylld
* Komponent för att visa felmeddelanden
* Visa bekräftelse när exemplar markerats som otillgängligt
* Validering av behörighet för att ändra
* Spara status och orsak för exemplar

---
### Att göra för implementation S-25:
* Hämta bokningar för (guid exemplarId)
* Skapa BookingService
* Filtrera till endast aktiva bokningar sluttid >= dagens datum
* Lista alla berörda bokningar (guid Id, string CustomerName, string CustomerEmail, string CustomerPhone, BookingPeriod bookingPeriod)
* Visa "Inga bokningar berörs" när listan är tom

---

## Diskussionsunderlag för möte inför itteration 2

En sak jag hade justerat i User Storyn  
Jag ser en liten inkonsekvens:  
Task 118 säger: första dagen är idag eller senare

medan AC1 säger: alla framtida aktiva bokningar

Skulle man istället kunna ändra till:

Bokningens startdatum >= dagens datum

eller

Bokningens slutdatum >= dagens datum

Det blir påverkan på implementatione

---

## Ändringar efter under period 2

* jag har nu insätt att jag gör bäst i att sätta att den kollar behörighet direkt, innan man får tillgång att sätta ett exemplar som otillgängligt. 
Jag måste göra de ändringarna i mina diagram, samt när jag implementerar koden. 

* Lägga till titel i diagrammen.

* Lägga till repeat om orsak inte är angiven

autonumber
activate
deactivate

---
3. Implementera och verifiera designen (VG)
För VG ska du dessutom implementera den modellerade funktionaliteten i ett objektorienterat programmeringsspråk (C#/.NET).

Implementationen ska innehålla de klasser, objekt och metoder som behövs för att genomföra det valda flödet. Ansvarsfördelningen och objektens samverkan ska gå att följa mellan koden och sekvensdiagrammet.

En enkel konsolapplikation räcker. Det krävs inget grafiskt gränssnitt, ingen databas och inga externa tjänster. Data får lagras i minnet.

Verifiera minst:

Ett scenario där huvudflödet genomförs.
Ett scenario där det alternativa flödet inträffar.
Du väljer själv om verifieringen sker genom manuella körningar, automatiserade tester eller en kombination. Automatiserade tester är inget krav. Koden som du skrivit ska finnas publicerat på en publik GitHub repository och länken till detta/dessa repositories ska finnas med i dokumentationen som lämnas in.

---
4. Analysera resultatet (VG)
Beskriv kortfattat:

Hur verifieringen visar att funktionaliteten följer aktivitetsdiagrammet.
Hur implementationens objekt och metodanrop motsvarar sekvensdiagrammet.
Om ansvarsfördelningen fungerade som tänkt.
Vilka förändringar du gjorde och varför.
Om implementationen visar att designen behöver ändras ska du uppdatera diagrammen. Redovisa vad som ändrades och motivera beslutet. Vilket är något som även tas med i grupparbetet sen under punkt 6.



Diskussion för iterationsmöte 2
jag tog bort VR 13 ifrån min feature då jag kände att den inte direkt var kopplad.
Jag la även till FK12 – Markera exemplar som otillgängligt
Koppling: K09
Systemet ska låta personal markera ett exemplar som otillgängligt och ange orsak.
i min feature 