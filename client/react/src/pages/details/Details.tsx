import "./details.css";
import "react-calendar/dist/Calendar.css";
import { useContext, useEffect, useState } from "react";
import Calendar from "react-calendar";
import { ImArrowLeft } from "react-icons/im";
import { GetHomeOfficeTimesByDayAsync } from "../../services/sercives";
import { HomeOfficeTime } from "../../types/types";
import { AppContext } from "../../context/AppContext";

type Props = {
  open: boolean;
  onClose: React.Dispatch<React.SetStateAction<boolean>>;
};

type DatePiece = Date | null;
type CalendarDate = DatePiece | [DatePiece, DatePiece];

export default function Details({ open, onClose }: Props) {
  const { user } = useContext(AppContext);
  const [currentDate, setCurrentDate] = useState<CalendarDate>(new Date());
  const [officeTimes, setOfficeTimes] = useState<HomeOfficeTime[]>([]);

  useEffect(() => {
    const loadCurrentHomeOfficeTime = async () => {
      try {
        const res = await GetHomeOfficeTimesByDayAsync(
          user?.id,
          currentDate?.toLocaleString()
        );
        if (res.status) {
          setOfficeTimes(res.data.result);
        }
      } catch (error) {
        console.log(error);
      }
    };

    loadCurrentHomeOfficeTime();
  }, [currentDate]);

  return (
    <div className={open ? "details active" : "details"}>
      <div className="details-wrapper">
        <h4>{`Übersicht für ${user?.userName}`}</h4>
        <button onClick={() => onClose(false)} className="nav-back-btn">
          <ImArrowLeft className="back-icon" /> Start/Stop
        </button>

        <div className="calendar-wrapper">
          <span>Datum wählen</span>
          <Calendar onChange={setCurrentDate} value={currentDate} />
        </div>

        <div className="times-wrapper">
          <span className="current-date">
            {currentDate?.toLocaleString("en-DE")}
          </span>
          <div className="current-date-times">
            {officeTimes.map((item) => (
              <span
                key={item?.id}
              >{`${item.startTime} - ${item.endTime} Uhr`}</span>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}
