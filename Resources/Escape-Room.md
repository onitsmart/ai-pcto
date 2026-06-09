# PCTO 2026 - Escape Room

Obiettivo: utilizzare il motore di AI per sviluppare una Escape Room che sia il più complicata possibile.

## Strumenti

Repository: https://github.com/onitsmart/ai-pcto 

Contiene un piccolo progetto console che permette di interfacciarsi con OpenAI tramite linea di comando. 
Perché funzioni, è necessario inserire la chiave API nei punti indicati (file: 'Program.cs').  

api-key per OpenAI: (fornita separatemente)

Nota: essendo la console a riga di comando, non sarà possibile fare uso di enigmi visivi come immagini o video. 

### Modifiche da effettuare nel progetto console

### file 'Program.cs'

Sostituire 

<code>
var apiKey = "insert-your-api-key-here"
</code>

con la chiave API che verrà fornita separatamente.

### file 'Chat.cs'

Sostituire l'implementazione di *var agent* e *var selectionFunction* con questo codice: 

<code>
var agent = new ChatCompletionAgent()
{
    Name = "BASE_AGENT",
    Instructions = """
        Answer as best as you can.
    """,
    Kernel = _kernel,
    Arguments = new KernelArguments(
        new OpenAIPromptExecutionSettings()
        {
            FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
        }),
};

var selectionFunction = KernelFunctionFactory.CreateFromPrompt(
    """
    You are the function that selects which agent to call.
    Available agents:
    {{$agents}}

    Conversation history:   
    {{$history}}

    Always return exactly this agent name and nothing else:
    BASE_AGENT
    """);
</code>

Per lo sviluppo dell'*Escape Room* i ragazzi dovranno modificare le *instructions* del *"BASE_AGENT"* (l'attuale *"Answer as best as you can."*) e NON la *selectionFunction*. 
L'inserimento del prompt con le istruzioni per gli enigmi nella *selectionFunction* causa un errore bloccante all'applicativo, con la sua conseguente immediata interruzione.

Idealmente, questa attività prevederebbe l'utilizzo di un *agent* per enigma, ma avendo poco tempo a disposizione è consigliato gestire tutto con un singolo *agent*.


## Sviluppo

I ragazzi lavoreranno in coppia o, se necessario, a gruppi di tre. 

Poiché la difficoltà delle prove non potrà essere troppo alta (probabilmente), ogni *Escape Room* dovrà contare almeno **sei** enigmi, di diverse tipologie.

Attenzione: l'ordine degli enigmi è a scelta della squadra sviluppatrice, ma in presenza di un enigma di 4° livello, questo potrà essere posizionato **solamente in ultima posizione**.  


### Livello degli enigmi 


#### Enigma di 1° livello 

In due parole:  **Intuizione e Osservazione**

Caratteristiche: 
L'enigma richiede un solo passaggio logico evidente. Tutti gli elementi necessari sono ben visibili e non nascosti.

Tipologia di gioco:
Associazioni dirette (es. collegare parole con una linea, trovare un oggetto nascosto ma palese, risolvere un labirinto semplice, un semplice puzzle a incastro).


#### Enigma di 2° livello

In due parole:  **Associazione e Pattern**

Caratteristiche: 
L'enigma richiede da 2 a 3 passaggi logici. I giocatori devono collegare due o più indizi trovati in posti diversi e riconoscere schemi (pattern).

Tipologia di gioco:
Semplici anagrammi, decifrazione con una chiave di lettura chiara (es. alfabeto cifrato), completamento di sequenze numeriche o logiche, utilizzo di un indizio per aprire un lucchetto a combinazione.


#### Enigma di 3° livello

In due parole:  **Deduzione e Ragionamento Laterale**

Caratteristiche: 
L'enigma richiede più passaggi (4 o più), l'esclusione di opzioni e una buona dose di ragionamento laterale. Richiede ai partecipanti di cambiare prospettiva.

Tipologia di gioco:
Enigmi in cui bisogna unire informazioni frammentarie (meta-puzzle), giochi di parole complessi, rebus, o indovinelli con trabocchetti logici.


#### Enigma di 4° livello

In due parole:  **Mastermind**

Caratteristiche: 
Richiede un elevato carico cognitivo, memoria a breve termine e la scomposizione di un problema grande in più sotto-problemi.

Tipologia di gioco:
Combinazione di più tecniche (es. un testo cifrato che diventa poi l'indizio per un secondo enigma), complessi calcoli matematici, o deduzioni in stile "tabella di verità".


## Sfida

Alla fine dello sviluppo, ogni gruppo si metterà alla prova sfidando l'*Escape Room* dei compagni (idealmente tutti le provano tutte, estrai all'inizio?). 

Volendo si può implementare anche un conteggio dei punti.

### Limite di tempo per evadere l'*Escape Room*

- enigma di 1° livello = 3 minuti
- enigma di 2° livello = 5 minuti
- enigma di 3° livello = 12 minuti
- enigma di 4° livello = 20 minuti

(Se si fa utilizzo dei punti, il rispetto dei tempi INCIDE sul punteggio finale!)

### Conteggio punti (facoltativo, vedi se è il caso)

#### Squadra sfidante

Obiettivo: evadere l'*Escape Room*

**Tutti i punti sono calcolati durante oppure dopo la sfida.**

Enigmi risolti in autonomia: 
- enigma di 1° livello = 2 pt.
- enigma di 2° livello = 4 pt.
- enigma di 3° livello = 9 pt.
- enigma di 4° livello = 15 pt.

Enigmi risolti con aiuti esterni (FORNITI DAGLI SFIDATI): 
- enigma di 1° livello = 1 pt. (MAX 1 aiuto)
- enigma di 2° livello = 2 pt. (MAX 1 aiuto)
- enigma di 3° livello = 5 pt. (MAX 1 aiuto)
- enigma di 4° livello = 13 pt. (1 aiuto) oppure 11 pt. (2 aiuti) oppure 9 pt. (3 aiuti)    

Uscita dall'*Escape Room* = 10 pt.

Relativi al tempo limite: 
- 1 pt. per ogni minuto rimasto nel timer
- -1 pt. per ogni minuto sforato
(L'ideale sarebbe contare sui singoli enigmi, vedi se è possibile)

#### Squadra sfidata

Obiettivo: incastrare gli sfidanti dentro l'*Escape Room*

**Valore base dell'*Escape Room*:**

Valore base di ogni enigma preparato per gli sfidanti: 
- enigma di 1° livello = 1 pt.
- enigma di 2° livello = 3 pt.
- enigma di 3° livello = 6 pt.
- enigma di 4° livello = 10 pt.

Valore di ogni aiuto preparato per gli sfidanti: 
- aiuto di 1° livello = 0.5 pt.
- aiuto di 2° livello = 1.5 pt.
- aiuto di 3° livello = 4 pt.
- aiuto di 4° livello = 5 pt. (1° aiuto) poi 3 pt. (2° aiuto) poi 2 pt. (3° aiuto)


**Punti calcolati dopo la sfida:**

Valore base degli enigmi non risolti dagli sfidanti: 
- enigma di 1° livello = 2 pt.
- enigma di 2° livello = 3 pt.
- enigma di 3° livello = 7 pt.
- enigma di 4° livello = 9 pt.

Valore base degli enigmi non risolti dagli sfidanti NONOSTANTE GLI AIUTI FORNITI: 
- enigma di 1° livello = 4 pt.
- enigma di 2° livello = 4 pt.
- enigma di 3° livello = 9 pt.
- enigma di 4° livello = 11 pt. (1 aiuto) oppure 13 pt. (2 aiuti) oppure 15 pt. (3 aiuti)

Gli sfidanti non riescono ad evadere l'*Escape Room* = 10 pt.



## Esempi di enigmi

### 1° livello (Intuizione e Osservazione)

- La Chiave a Specchio: 
Trovare una chiave appesa sul retro di un oggetto visibile solo guardando dentro uno specchio.

- L'Intruso Cromatico: 
Individuare l'unico libro rosso in mezzo a una fila di libri interamente blu per trovare un foglietto.

- L'Unione dei Puntini: 
Unire i numeri da 1 a 10 stampati su una mappa per disegnare la forma di un oggetto geometrico.

- Il Labirinto Tracciato: 
Seguire l'unica linea continua su un tabellone per collegare un personaggio alla sua porta corretta.

### 2° livello (Associazione e Pattern)

- Il Cifrario di Cesare a Vista: 
Decifrare una parola sul muro usando una ruota alfabetica trovata sul tavolo.

- La Data sul Calendario: 
Trovare un diario con un giorno cerchiato e usare quel giorno/mese per sbloccare un lucchetto a 4 cifre.

- La Sequenza delle Candele: 
Accendere quattro candele elettroniche seguendo l'ordine di altezza (dalla più bassa alla più alta) indicato in un disegno.

- L'Anagramma del Quadro: 
Riordinare le lettere evidenziate nella targhetta di un dipinto per formare il nome del cassetto da aprire.

### 3° livello (Deduzione e Ragionamento Laterale)

- Il Cruciverba Senza Definizioni: 
Riempire una griglia vuota incrociando parole sparse trovate nella stanza, deducendone la posizione solo dalla lunghezza delle lettere.

- L'Ombra Proiettata: 
Ruotare un oggetto tridimensionale astratto davanti a una fonte di luce finché la sua ombra sul muro non rivela un numero romano.

- Il Racconto Bugiardo: 
Leggere la confessione di tre sospettati e trovare l'unica contraddizione logica temporale per capire chi mente e svelare il codice.

- La Poesia Direzionale: 
Interpretare una filastrocca dove parole come "orizzonte", "cadere", "passato" e "futuro" indicano i movimenti (Su, Giù, Sinistra, Destra) di un joystick.

### 4° livello (Mastermind)

- Il Meta-Puzzle degli Elementi: 
Risolvere tre enigmi indipendenti (uno visivo, uno matematico, uno testuale) per ottenere tre sostanze chimiche, da inserire poi in una tavola periodica per generare il codice finale.

- La Griglia di Deduzione Logica (Stile Einstein): 
Incrociare cinque indizi frammentari per accoppiare cinque personaggi a cinque chiavi diverse, scoprendo chi possiede quella reale tramite esclusione.

- Il Tabellone Elettrico Cifrato: 
Ripristinare i collegamenti di un finto circuito elettrico risolvendo prima un enigma matematico; i fili corretti formeranno visivamente lettere scritte in un alfabeto inventato da tradurre.

- Il Canto dei Rintocchi: 
Ascoltare una traccia audio con suoni ambientali, contare la frequenza di rintocchi specifici in determinati minuti e usare i totali come coordinate per isolare parole su una pagina di giornale.

