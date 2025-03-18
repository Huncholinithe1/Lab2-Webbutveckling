const express = require('express');
const cors = require('cors');
const axios = require('axios');
const path = require('path');

const app = express();
const port = process.env.PORT || 3000;

// URL till .NET API:et på Azure
const DOTNET_API_URL = 'https://app-review-yusuf-exeebbffbsavf5dz.swedencentral-01.azurewebsites.net';

// Middleware-konfiguration
app.use(cors());
app.use(express.json());
app.use(express.static(path.join(__dirname, 'public')));

// Servera HTML-sidan
app.get('/', (req, res) => {
    res.sendFile(path.join(__dirname, 'public', 'index.html'));
});

// Hämta alla recensioner
app.get('/api/recensioner', async (req, res) => {
    try {
        const response = await axios.get(`${DOTNET_API_URL}/recensioner`);
        res.json(response.data);
    } catch (error) {
        console.error('Fel vid hämtning av recensioner:', error);
        res.status(500).json({ error: 'Kunde inte hämta recensioner' });
    }
});

// Hämta en specifik recension
app.get('/api/recensioner/:id', async (req, res) => {
    try {
        const response = await axios.get(`${DOTNET_API_URL}/recensioner/${req.params.id}`);
        res.json(response.data);
    } catch (error) {
        res.status(500).json({ error: 'Kunde inte hämta recensionen' });
    }
});

// Skapa en ny recension
app.post('/api/recensioner', async (req, res) => {
    try {
        const response = await axios.post(`${DOTNET_API_URL}/recensioner`, req.body);
        res.status(201).json(response.data);
    } catch (error) {
        console.error('Fel vid skapande av recension:', error);
        res.status(500).json({ error: 'Kunde inte skapa recensionen' });
    }
});

// Uppdatera en recension
app.put('/api/recensioner/:id', async (req, res) => {
    try {
        await axios.put(`${DOTNET_API_URL}/recensioner/${req.params.id}`, req.body);
        res.status(204).send();
    } catch (error) {
        console.error('Fel vid uppdatering av recension:', error);
        res.status(500).json({ error: 'Kunde inte uppdatera recensionen' });
    }
});

// Ta bort en recension
app.delete('/api/recensioner/:id', async (req, res) => {
    try {
        await axios.delete(`${DOTNET_API_URL}/recensioner/${req.params.id}`);
        res.status(200).send();
    } catch (error) {
        console.error('Fel vid borttagning av recension:', error);
        res.status(500).json({ error: 'Kunde inte ta bort recensionen' });
    }
});

// Starta servern
app.listen(port, () => {
    console.log(`Express API körs på http://localhost:${port}`);
    console.log(`Ansluter till .NET API på: ${DOTNET_API_URL}`);
}); 