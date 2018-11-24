# PacketDetection

Cel aplikacji: stworzenie symulacji pozwalającej na badanie działania kontroli błędu 
              (CRC, suma kontrolna, bit parzystości) na różny typ zakłóceń oraz strukturę przesyłanych danych.
              

Aplikacja pozwala na:
- określenie długości wysyłanej ramki/pakietu,
- określenie liczby ramek w pakiecie,
- określenie długości części kontrolnej,
- określenie typu kontroli dla wysyłanego pakietu/ramki (CRC, suma kontrolna, bit parzystości),
- określenie sposobu zakłóceń wpływających na pakiet/ramkę (funkcja sinus, grupa bitów, losowo),
- określenie liczby przesyłanych pakietów,
- określenie czasu działania symulacji,
- uzyskanie informacji na temat działania kontroli przesyłanych pakietów (4 stany).


Pakiet/Ramka przechodzi przez 3 etapy.
1.	Generowanie części kontrolnej ze względu na dane oraz ze względu na typ kontroli.
2.	Symulacja zakłóceń (funkcja sinus, grupa bitów, losowo).
3.	Sprawdzenie poprawności danych.


Na potrzeby określenia działania kontroli zostały wyznaczone 4 stany, gdy dane 
w pakiecie/ramce:
1.	Nie zostały przekłamane oraz typ kontroli uznaje dane za bezbłędne.
2.	Zostały przekłamane oraz zostało to wykryte.
3.	Zostały przekłamane oraz nie zostało to wykryte.
4.	Nie zostały przekłamane, ale zostały uznane za błędne 
(najczęściej spowodowane przez ograniczenia długości części kontrolnej -suma kontrolna. 
Pomaga również w ewentualnym wykrywaniu błędów w algorytmie.)


Do zrobienia:
1. Polepszenie kolizji bazującej na funkcji sinus. Problem głównie związany z przybliżeniami liczb zmiennoprzecinkowymi.
2. Dodanie zabezpieczeń związanych z wyłączeniem/włączaniem aplikacji. 
3. Dodanie możliwości zmiany prowadzonej transmisji. (np. przycisk „reset” )
4. Przetestowanie działania aplikacji. Zostały jedynie wykonane testy jednostkowe sprawdzające niektóre funkcje.
5. Dodanie algorytmów bazujących na bicie parzystości pozwalających na wygenerowanie części kontrolnej tego typu o długości większej niż 1 bit.
6. Podzielenie menu na części odpowiadające konkretnemu typu kolizji(dodanie interfejsu, dziedziczenie, ogólne polepszenie kodu)
