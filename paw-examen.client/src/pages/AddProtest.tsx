import React, { useState, useEffect } from 'react';
import axios from "../api/axiosConfig";

interface Locatie {
    id: number;
    judet: string;
    oras: string;
    numar_locuitori: number;
}

const AddProtest = () => {
    const [denumire, setDenumire] = useState('');
    const [descriere, setDescriere] = useState('');
    const [numar_participanti, setNumarParticipanti] = useState('');
    const [data, setData] = useState('');
    const [locatieId, setLocatieId] = useState('');
    const [locatii, setLocatii] = useState<Locatie[]>([]);
    const [error, setError] = useState('');

    useEffect(() => {
        const fetchLocatii = async () => {
            try {
                const response = await axios.get('/api/locatie');
                setLocatii(response.data);
            } catch (error) {
                console.error('There was an error fetching the locatii!', error);
            }
        };

        fetchLocatii();
    }, []);

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const selectedData = new Date(data)
        const selectedLocatie = locatii.find(l => l.id === parseInt(locatieId));

        if (!selectedLocatie) {
            alert('Please select a valid Locatie.');
            return;
        }

        try {
            const newProtest = {
                denumire,
                descriere,
                numar_participanti,
                data: selectedData.toISOString(),
                locatieId: selectedLocatie.id,
            };

            await axios.post('/api/protest', newProtest);
            alert('Protest added successfully');
            setDenumire('');
            setDescriere('');
            setNumarParticipanti('');
            setData('');
            setLocatieId('');
        } catch (error) {
            console.error('There was an error adding the Protest!', error.response);
            setError(error.response.data)
        }
    };

    return (
        <div>
            <h2>Add New Protest</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Denumire:</label>
                    <input type="text" value={denumire} onChange={(e) => setDenumire(e.target.value)} required />
                </div>
                <div>
                    <label>Descriere:</label>
                    <input type="text" value={descriere} onChange={(e) => setDescriere(e.target.value)} required />
                </div>
                <div>
                    <label>Numar Participanti:</label>
                    <input type="text" value={numar_participanti} onChange={(e) => setNumarParticipanti(e.target.value)} required />
                </div>
                <div>
                    <label>Data:</label>
                    <input type="date" value={data} onChange={(e) => setData(e.target.value)} required />
                </div>
                <div>
                    <label>Locatie:</label>
                    <select value={locatieId} onChange={(e) => setLocatieId(e.target.value)} required>
                        <option value="">Select a Locatie</option>
                        {locatii.map((l) => (
                            <option key={l.id} value={l.id}>
                                {l.oras}
                            </option>
                        ))}
                    </select>
                </div>
                <p>{error}</p>
                <button type="submit">Add Protest</button>
            </form>
        </div>
    );
};

export default AddProtest;
