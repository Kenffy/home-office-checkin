import { FormEvent, useContext, useRef, useState } from "react";
import "./login.css";
import { AppContext } from "../../context/AppContext";
import { LoginAsync } from "../../services/sercives";

export type LoginProps = {
  UserName: string;
  Password: string;
};

export default function Login() {
  const { setUser } = useContext(AppContext);
  const [errorMessage, setErrorMessage] = useState<string>("");

  const userNameRef = useRef<HTMLInputElement>(null);
  const passwordRef = useRef<HTMLInputElement>(null);

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();

    const username = userNameRef.current?.value.trim();
    const password = passwordRef.current?.value.trim();

    if (!username || !password) {
      setErrorMessage("Username and password are required.");
      handleClearInputs();
      return;
    }

    try {
      const res = await LoginAsync({ UserName: username, Password: password });
      if (res.status == 200) {
        setUser(res.data.result);
        handleClearInputs();
      }
    } catch (error) {
      console.log(error);
      setErrorMessage("Oop! Something went wrong!");
    }
  };

  const handleClearInputs = () => {
    if (passwordRef.current) {
      passwordRef.current.value = "";
    }
    if (userNameRef.current) {
      userNameRef.current.value = "";
    }
  };

  return (
    <div className="login">
      <div className="wrapper">
        <h2>HomeOffice Checkin - Login</h2>
        <span className="error-msg">{errorMessage}</span>
        <form className="login-form" onSubmit={handleSubmit}>
          <input
            ref={userNameRef}
            required
            type="text"
            placeholder="Username"
            onFocus={() => setErrorMessage("")}
          />
          <input
            ref={passwordRef}
            required
            type="password"
            placeholder="Password"
            onFocus={() => setErrorMessage("")}
          />
          <button type="submit">Login</button>
        </form>
      </div>
    </div>
  );
}
