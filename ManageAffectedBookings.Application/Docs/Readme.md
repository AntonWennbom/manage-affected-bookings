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

### Sekvensdiagram SD-24

```plantuml

@startuml

actor Personal

participant Frontend
participant Backend
database Database

Personal -> Frontend : Markera exemplar\nsom otillgängligt
Personal -> Frontend : Ange orsak

Frontend -> Backend : MarkeraOtillgänglig(exemplarId, orsak)

alt Orsak saknas

    Backend --> Frontend : Valideringsfel
    Frontend --> Personal : Orsak måste anges

else Orsak finns

    Backend -> Backend : Kontrollera behörighet

    alt Behörighet saknas

        Backend --> Frontend : 403 Forbidden
        Frontend --> Personal : Åtkomst nekad

    else Behörighet OK

        Backend -> Database : Uppdatera status
        Backend -> Database : Spara orsak

        Database --> Backend : OK

        note over Backend
        VR10:
        Bokningar får inte
        avbokas automatiskt
        end note

        Backend --> Frontend : Exemplar uppdaterat

        Frontend --> Personal : Status sparad

    end

end

@enduml

```

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
### Att göra för implementation:
* 

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

Det blir påverkan på implementationen

---