# 🎬 CinemaGamma

**Web App per la gestione di proiezioni cinematografiche** sviluppata in .NET Core con **C#**, **Razor Pages**, **Keycloak** per l'autenticazione, e **Docker** per il deploy containerizzato.

---

## 🧩 Tecnologie utilizzate

- **Backend:** ASP.NET Core (.NET 8), C#
- **Frontend:** Razor Pages
- **Database:** SQL Server (via Entity Framework)
- **Autenticazione:** Keycloak (JWT)
- **Containerizzazione:** Docker
- **Dependency Injection & Logging:** built-in .NET
- **API e Sicurezza:** Authorization via JWT, Swagger/OpenAPI

---

## 🚀 Funzionalità principali

- Gestione di film, sale e orari di proiezione
- Sistema di prenotazione integrato
- Transazioni simulate
- Login sicuro tramite Keycloak
- Protezione degli endpoint via autorizzazioni (Bearer Token)
- Ambiente multi-layer con logging e gestione errori centralizzata

---

## 🔐 Autenticazione Keycloak

La Web API utilizza **Keycloak** per la gestione centralizzata dell'identità e dei permessi.  
Le configurazioni principali si trovano in `appsettings.json`:

```json
"KEYCLOAK_AUTHORITY": "https://<tuo-dominio-keycloak>/realms/CinemaGamma",
"KEYCLOAK_CLIENT_ID": "cinemagamma-backend"

