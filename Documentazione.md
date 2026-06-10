# SETUP INIZIALE CON VSC e VERSIONING GIT GITHUB

1 - Installare DOTNET SDK 10.0 dal sito ufficiale:  
   https://dotnet.microsoft.com/en-us/download/dotnet/10.0

2 - Installare VISUAL STUDIO CODE (VSC) dal sito ufficiale:  
   https://code.visualstudio.com/

3 - Installare GIT dal sito ufficiale:  
   https://git-scm.com/downloads

4 - Installare GITHUB CLI dal sito ufficiale:  
   https://cli.github.com/

5 - Installare L'estensione DOTNET instant tools per VSC e C:  
   Aprire VSC -> Estensioni (icona a forma di quadrato con 4 quadratini più piccoli) -> Cercare "dotnet" -> Installare "C# Dev Kit" di Microsoft

## Versionamento

Il versionamento è un sistema che permette di tenere traccia delle modifiche apportate al codice sorgente di un progetto nel tempo. In questo modo è possibile tornare indietro a versioni precedenti del codice, confrontare le differenze tra le versioni e collaborare con altri sviluppatori.

**I vantaggi principali del versionamento sono:**

- Permette di tenere traccia delle modifiche apportate al codice sorgente nel tempo
- Facilita la collaborazione tra sviluppatori, permettendo di lavorare su versioni diverse del codice senza conflitti
- Permette di tornare indietro a versioni precedenti del codice in caso di errori o problemi
- Facilita la gestione delle dipendenze tra i progetti, permettendo di tenere traccia delle modifiche apportate alle librerie utilizzate nel progetto.
- Permette di creare branch per sviluppare nuove funzionalità o correggere bug senza influire sulla versione stabile del codice.

**I termini più comuni utilizzati nel versionamento sono:**

- Repository: è il luogo dove viene archiviato il codice sorgente del progetto e tutte le sue versioni.
- Commit: è un'istantanea del codice sorgente in un determinato momento, che contiene un messaggio descrittivo delle modifiche apportate.
- Branch: è una linea di sviluppo separata dalla versione principale del codice, che permette di sviluppare nuove funzionalità o correggere bug senza influire sulla versione stabile del codice.
- Merge: è l'operazione di unire le modifiche apportate in un branch con la versione principale del codice.
- Pull: è l'operazione di scaricare le modifiche apportate da altri sviluppatori al repository remoto e unirle con la versione locale del codice.
- Pull Request: è una richiesta di revisione del codice che viene inviata quando si vuole unire le modifiche apportate in un branch con la versione principale del codice, permettendo ad altri sviluppatori di rivedere le modifiche prima di unirle al codice principale.

## Configurare GIT e GITHUB CLI

Aprire il terminale (cmd, powershell, bash, ecc) e digitare i seguenti comandi:

```bash
git config --global user.name "TUO_NOME_UTENTE_GITHUB"
git config --global user.email "TUO_EMAIL_GITHUB"

# per verificare la configurazione
git config --global user.name
git config --global user.email

gh auth login
# seguire la procedura guidata per autenticarsi con GITHUB
```

A questo punto dovremmo essere riconosciuti come utenti GITHUB:

```bash
gh auth status
```

## Creazione del Repository su GITHUB

1 - Creazione locale della WorkingFolder nella quale saranno contenuti **più repository**:

2 - Creazione guidata del repository tramite il sito github

## Creazione del file .gitignore

Creare un file di testo chiamato `.gitignore` nella root del repository e aggiungere le seguenti righe per escludere i file e le cartelle comuni che non devono essere tracciati da Git:

```
# Visual Studio Code
.vscode/

# In modo specifico per progetti dornet
bin/
obj/

# Per evitare i files DS_Store su macOS
.DS_Store
```

## Primo Commit e Push su GITHUB

A questo punto siamo pronti per effettuare il primo commit e push del nostro repository su GITHUB:

```bash
git add --all
git commit -m "Primo commit - Setup iniziale"
git push origin main
```

Dopo avere mofiicato un file, possiamo verificare lo stato del repository con:

```bash
git status
```

- Inizialmente stampa i file modificati e non ancora tracciati in rosso.
- Dopo avere aggiunto i file con `git add --all`, i file modificati vengono mostrati in verde.

## Clonazione repository esistente da GITHUB

Per clonare un repository esistente da GITHUB, aprire il terminale nella cartella dove si vuole clonare il repository e digitare il seguente comando:

```bash
gh repo clone NOME_UTENTE/NOME_REPOSITORY
```

In ogni caso è sufficiente cliccare su Code -> Local -> Github CLI.

## Creazione file Solution

Il file solution (.slnx) è un file che contiene informazioni sui progetti inclusi in una soluzione .NET.

- Implementa funzionalità aggiuntive come l'autocompletamento e la gestione delle dipendenze tra progetti e la segnalazione degli errori.

IMPORTANTE: Bisogna riavviare il computer per permettere a VSC di riconoscere il file solution.

Il file deve essere creato nella root cioè nella **WorkingFolder**.
Per creare un file solution, aprire il terminale nella root del repository e digitare il seguente comando:

```bash
dotnet new sln -n Solution
```

A questo punto tuuti i repository dentro la WorkingFolder avranno un file solution condiviso.

## Comandi Powershell utili

```bash
# Per spostarsi tra le cartelle
cd NomeCartella
cd ..
# per creare una cartella
mkdir NomeCartella
# per visualizzare il contenuto della cartella
dir
# per pulire la console
cls o clear
```

Questi comandi si possono usare in qualsiasi terminale (cmd, powershell, bash, ecc).

Git Bash supporta tutti i comandi di bash e anche quelli di powershell e in più visualizza sempre il percorso completo della cartella corrente e il branch remoto attivo.

## Creazione della cartella Esercitazioni

Nella root della WorkingFolder creare una cartella chiamata `Esercitazioni` dove andranno creati tutti i progetti di esercitazione.
```bash
mkdir Esercitazioni
cd Esercitazioni
```

## Creazione progetto console dotnet

Si possono creare diversi tipi di progetto a seconda dello scopo e si posso visualizzare con il comando:

```bash
dotnet new --list
```

Nel nostro caso creo un progetto console cioè un'applicazione che viene eseguita da riga di comando:

```bash
# se voglio che venga creata anche la cartella del progetto
dotnet new console -o NomeProgettoConsole
# invece voglio creare solo l'applicazione:
dotnet new console
```

Vengono creati i seguenti file:
- Program.cs: il file principale dove viene scritto il codice C# (entry point dell'applicazione)
- NomeProgettoConsole.csproj: il file di progetto che contiene le informazioni sul progetto e le dipendenze
- Bin e Obj: cartelle che contengono i file compilati e temporanei

## Aggiungere progetti al file Solution

Per aggiungere un progetto esistente al file solution, aprire il terminale nella root del repository e digitare il seguente comando:

```bash
dotnet sln Solution.slnx add Percorso/Del/Progetto/Progetto.csproj
```

A questo punto possiamo utilizzare le funzionalità come autocompletamento.

IMPORTANTE: il comando deve essere eseguito nella root della WorkingFolder dove è stato creato il file solution.

## Eseguire il progetto
Per eseguire il progetto, aprire il terminale nella cartella del progetto e digitare il seguente comando:

IMPORTANTE: Salvare sempre prima il progetto in VSC.

```bash
dotnet run
```