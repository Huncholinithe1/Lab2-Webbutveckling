const express = require('express');
const cors = require('cors');
const axios = require('axios');
const path = require('path');

const app = express();
const port = process.env.PORT || 3000;

// URL till .NET API:et på Azure
const DOTNET_API_URL = 'https://app-review-yusuf-exeebbffbsavf5dz.swedencentral-01.azurewebsites.net';

// CORS-konfiguration
app.use(cors({
    origin: '*',
    methods: ['GET', 'POST', 'PUT', 'DELETE'],
    allowedHeaders: ['Content-Type', 'Accept']
}));

// Middleware-konfiguration
app.use(express.json());
app.use(express.static(path.join(__dirname, 'public')));

// Logga alla requests
app.use((req, res, next) => {
    console.log(`${new Date().toISOString()} - ${req.method} ${req.url}`);
    next();
});

// Servera HTML-sidan
app.get('/', (req, res) => {
    res.sendFile(path.join(__dirname, 'public', 'index.html'));
});

// Hämta alla recensioner
app.get('/api/recensioner', async (req, res) => {
    try {
        console.log('Försöker hämta recensioner från:', `${DOTNET_API_URL}/recensioner`);
        const response = await axios.get(`${DOTNET_API_URL}/recensioner`);
        console.log('Svar från API:', response.data);
        res.json(response.data);
    } catch (error) {
        console.error('Fel vid hämtning av recensioner:', error.response ? error.response.data : error.message);
        res.status(500).json({ error: 'Kunde inte hämta recensioner', details: error.message });
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
        console.log('Försöker skapa recension:', req.body);
        const response = await axios.post(`${DOTNET_API_URL}/recensioner`, req.body);
        console.log('Recension skapad:', response.data);
        res.status(201).json(response.data);
    } catch (error) {
        console.error('Fel vid skapande av recension:', error.response ? error.response.data : error.message);
        res.status(500).json({ 
            error: 'Kunde inte skapa recensionen', 
            details: error.message,
            requestBody: req.body 
        });
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
        console.log('Försöker ta bort recension:', req.params.id);
        await axios.delete(`${DOTNET_API_URL}/recensioner/${req.params.id}`);
        console.log('Recension borttagen');
        res.status(200).send();
    } catch (error) {
        console.error('Fel vid borttagning av recension:', error.response ? error.response.data : error.message);
        res.status(500).json({ error: 'Kunde inte ta bort recensionen', details: error.message });
    }
});

// Starta servern
app.listen(port, () => {
    console.log(`Express API körs på http://localhost:${port}`);
    console.log(`Ansluter till .NET API på: ${DOTNET_API_URL}`);
}); 