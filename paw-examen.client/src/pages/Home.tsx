import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import axios from "../api/axiosConfig";

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
  numar_locuitori: string;
  data: string;
  locatie: Locatie;
}

const Home = () => {
  const [proteste, setCompanii] = useState<Protest[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [sortOrder, setSortOrder] = useState<"asc" | "desc">("asc");
  const navigate = useNavigate();
  useEffect(() => {
    axios.get("/api/protest")
      .then((response) => {
        setCompanii(response.data);
        setLoading(false);
      })
      .catch((error) => {
        setError("There was an error fetching the proteste!");
        console.error(error);
        setLoading(false);
      });
  }, []);

  useEffect(() => {
    if (proteste.length > 0) {

      setCompanii(prevProteste => [...prevProteste].sort((a, b) => {
        const countA = a.locatie.numar_locuitori;
        const countB = b.locatie.numar_locuitori;
        return sortOrder === "asc" ? countA - countB : countB - countA;
      }));
    }
  }, [proteste, sortOrder]);

  const handleDelete = (id: number) => {
    if (window.confirm("Are you sure you want to delete this protest?")) {
      axios.delete(`/api/protest/${id}`)
        .then(() => {
          setCompanii(proteste.filter(protest => protest.id !== id));
        })
        .catch((error) => {
          console.error("There was an error deleting the protest!", error);
        });
    }
  };

  const toggleSortOrder = () => {
    setSortOrder(prevOrder => (prevOrder === "asc" ? "desc" : "asc"));
  };

  if (loading) return <p>Loading...</p>;
  if (error) return <p>{error}</p>;

  return (
    <div>
      <p>Home</p>
      <div>
          <a href="/add-protest">Add a New Protest</a>
      </div>
      <hr/>
      <button onClick={toggleSortOrder}>
        Sorted by {sortOrder === "asc" ? "least" : "most"} Locatie numar locuitori
      </button>
      <ul>
        {proteste.map((protest: any) => (
          <li key={protest.id}>
            <h2>{protest.denumire}</h2>
            <p>Descriere: {protest.descriere}</p>
            <p>Numar participanti: {protest.numar_participanti}</p>
            <p>
              Data protestului:{" "}
              {new Date(protest.data).toLocaleDateString()}
            </p>
            <p>Locatia protestului: {protest.locatie.oras}</p>
            <button onClick={() => navigate(`/edit-protest/${protest.id}`)}>Edit</button>
            <button onClick={() => handleDelete(protest.id)}>Delete</button>
          </li>
        ))}
        
      </ul>
    </div>
  );
};

export default Home;
