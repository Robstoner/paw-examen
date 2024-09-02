import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import axios from "../api/axiosConfig";

// Define TypeScript interfaces
interface Locatie {
  id: number;
  judet: string;
  oras: string;
  numar_locuitori: number;
}

interface Protest {
  id: number;
  denumire: string;
  descriere: string;
  numar_participanti: string;
  data: string;
  locatieId: number;
}

const EditProtest = () => {
  const { id } = useParams<{ id: string }>();
  const [protest, setProtest] = useState<Protest | null>(null);
  const [locatii, setLocatii] = useState<Locatie[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    axios.get(`/api/protest/${id}`)
      .then((response) => {
        setProtest(response.data);
        setLoading(false);
      })
      .catch((error) => {
        setError("There was an error fetching the protest data!");
        console.error(error);
        setLoading(false);
      });

    axios.get('/api/locatie')
      .then((response) => {
        setLocatii(response.data);
      })
      .catch((error) => {
        console.error('There was an error fetching the locatii!', error);
      });
  }, [id]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    if (protest) {
      const { name, value } = e.target;
      setProtest({ ...protest, [name]: value });
    }
  };

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (protest) {
      axios.put(`/api/protest/${id}`, protest)
        .then(() => {
          navigate("/");
        })
        .catch((error) => {
          console.error("There was an error updating the protest!", error);
          setError(error.response.data);
        });
    }
  };

  if (loading) return <p>Loading...</p>;
  if (error) return <p>{error}</p>;

  return (
    <div>
      <h2>Edit Protest</h2>
      {protest && (
        <form onSubmit={handleSubmit}>
          <label>
            Denumire:
            <input
              type="text"
              name="denumire"
              value={protest.denumire}
              onChange={handleChange}
              required
            />
          </label>
          <br />
          <label>
            Descriere:
            <input
              type="text"
              name="descriere"
              value={protest.descriere}
              onChange={handleChange}
              required
            />
          </label>
          <br />
          <label>
            Numar participanti:
            <input
              type="text"
              name="numar_participanti"
              value={protest.numar_participanti}
              onChange={handleChange}
              required
            />
          </label>
          <br />
          <label>
            Data :
            <input
              type="date"
              name="data"
              value={new Date(protest.data).toISOString().substring(0, 10)}
              onChange={handleChange}
              required
            />
          </label>
          <br />
          <label>
            Locatii:
            <select
              name="locatieId"
              value={protest.locatieId}
              onChange={handleChange}
              required
            >
              <option value="">Select a Protest</option>
              {locatii.map((l) => (
                <option key={l.id} value={l.id}>
                  {l.oras}
                </option>
              ))}
            </select>
          </label>
          <br />
          <p>{error}</p>
          <button type="submit">Update</button>
        </form>
      )}
    </div>
  );
};

export default EditProtest;
