import { Link } from "react-router-dom";
import "./home.css";
import { FaPlay } from "react-icons/fa";
import { FaStop } from "react-icons/fa";
import { useContext, useEffect, useState } from "react";
import Details from "../details/Details";
import { AppContext } from "../../context/AppContext";
import {
  GetHomeOfficeTimeAsync,
  StartHomeOfficeAsync,
  StopHomeOfficeAsync,
} from "../../services/sercives";
import { HomeOfficeTime } from "../../types/types";
import { getTime } from "../../helpers/utils";

export default function Home() {
  const { user, setUser } = useContext(AppContext);

  const [openModal, setOpenModal] = useState<boolean>(false);
  const [onPlay, setOnPlay] = useState<boolean | undefined>(false);
  const [currentDate, setCurrentDate] = useState<Date>(new Date());
  const [currentHomeOfficeTime, setCurrentHomeOfficeTime] =
    useState<HomeOfficeTime | null>(null);

  useEffect(() => {
    const loadCurrentHomeOfficeTime = async () => {
      try {
        const res = await GetHomeOfficeTimeAsync(user?.id);
        if (res.status) {
          setCurrentHomeOfficeTime(res.data.result);
          setOnPlay(true);
        }
      } catch (error) {
        console.log(error);
      }
    };

    loadCurrentHomeOfficeTime();
  }, []);

  const handleHomeOfficeStart = async () => {
    try {
      const date = new Date();
      const currentTime: HomeOfficeTime = {
        id: 0,
        startTime: getTime(date),
        endTime: "",
        createdAt: date,
        updatedAt: null,
        userId: user?.id,
      };
      const res = await StartHomeOfficeAsync(currentTime);
      if (res.status == 200) {
        setOnPlay(true);
        setCurrentHomeOfficeTime(res.data.result);
      }
    } catch (error) {
      console.log(error);
    }
  };

  const handleHomeOfficeStop = async () => {
    if (!currentHomeOfficeTime) return;

    try {
      const date = new Date();
      const currentTime = currentHomeOfficeTime;
      currentTime.updatedAt = date;
      currentTime.endTime = getTime(date);
      const res = await StopHomeOfficeAsync(currentTime);
      if (res.status == 200) {
        setOnPlay(false);
        setCurrentHomeOfficeTime(null);
      }
    } catch (error) {
      console.log(error);
    }
  };

  const handleLogout = () => {
    setUser(null);
  };

  return (
    <div className="home">
      <nav>
        <div className="nav-wrapper">
          <Link to="/">
            <h2>HomeOffice Checkin</h2>
          </Link>

          <div>
            {user && <span className="user">{`Hello ${user.userName}`}</span>}
            {!user ? (
              <button>
                <Link to="/login">Login</Link>
              </button>
            ) : (
              <button onClick={handleLogout}>Logout</button>
            )}
          </div>
        </div>
      </nav>

      <div className="actions-panel">
        <div className="actions">
          <button
            onClick={handleHomeOfficeStart}
            disabled={onPlay}
            className="start-btn"
          >
            <FaPlay className="icon" />
          </button>
          <button
            onClick={handleHomeOfficeStop}
            disabled={!onPlay}
            className="stop-btn"
          >
            <FaStop className="icon" />
          </button>
        </div>
        <button onClick={() => setOpenModal(true)}>Übersicht</button>
      </div>

      <Details open={openModal} onClose={setOpenModal} />
    </div>
  );
}
