import { Routes, Route, Navigate } from "react-router-dom";
import Home from "./pages/home/Home";
import Login from "./pages/login/Login";
import { useContext } from "react";
import { AppContext } from "./context/AppContext";

type Props = {};

export default function App({}: Props) {
  const { user } = useContext(AppContext);

  return (
    <Routes>
      <Route path="/" element={user ? <Home /> : <Login />} />
      <Route path="login" element={user ? <Home /> : <Login />} />
      <Route path="*" element={<Navigate to="/" replace />} />
    </Routes>
  );
}
