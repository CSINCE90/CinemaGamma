using Microsoft.AspNetCore.Mvc;
using CinemaFrontend.Models;
using System;
using CinemaFrontend.DTO;


namespace CinemaFrontend.Controllers
{
    public class TransactionController : Controller
    {
        [HttpGet]
        public IActionResult Purchase(int reservationId, decimal amount)
        {
            var transaction = new TransactionDTO
            {
                ReservationId = reservationId,
                Amount = amount,
                TransactionDate = DateTime.Now
            };

            return View(transaction);
        }

        [HttpPost]
        public IActionResult Purchase(TransactionDTO transaction)
        {
            if (ModelState.IsValid)
            {
                // Qui inseriresti la logica per salvare la transazione nel database
                // Per ora, simuliamo un'operazione di salvataggio di successo
                transaction.IsSuccessful = true;
                transaction.Id = new Random().Next(1000, 9999); // Simula un ID generato

                // Redirect alla pagina di conferma
                return RedirectToAction("Confirmation", new { id = transaction.Id });
            }

            // Se il modello non è valido, ritorna alla vista con gli errori
            return View(transaction);
        }

        public IActionResult Confirmation(int id)
        {
            // In un'applicazione reale, qui recupereresti la transazione dal database usando l'id
            // Per ora, creiamo una transazione di esempio
            var transaction = new TransactionDTO
            {
                Id = id,
                ReservationId = 123, // Esempio
                Amount = 20.0m,
                PaymentMethod = "Carta di Credito",
                IsSuccessful = true,
                TransactionDate = DateTime.Now
            };

            return View(transaction);
        }
    }
}