import { BrowserRouter, Route, Routes } from 'react-router-dom';
import './App.css';
import Home from './pages/Home';
import AddProtest from './pages/AddProtest';
import EditProtest from './pages/EditProtest';


function App() {


    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/add-protest" element={<AddProtest />} />
                <Route path="/edit-protest/:id" element={<EditProtest />} />
            </Routes>
        </BrowserRouter>
    );

}

export default App;