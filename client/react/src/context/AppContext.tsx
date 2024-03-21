import {
  createContext,
  Dispatch,
  SetStateAction,
  ReactNode,
  useState,
  useEffect,
} from "react";
import { Employee } from "../types/types";

export interface UserContextInterface {
  user: Employee | null;
  setUser: Dispatch<SetStateAction<Employee | null>>;
}

const INIT_STATE = {
  user: JSON.parse(localStorage.getItem("ho-user") || ""),
  setUser: () => {},
} as UserContextInterface;

type UserProviderProps = {
  children: ReactNode;
};

export const AppContext = createContext(INIT_STATE);

export const AppProvider = ({ children }: UserProviderProps) => {
  const [user, setUser] = useState<Employee | null>(INIT_STATE.user);

  useEffect(() => {
    localStorage.setItem("ho-user", JSON.stringify(user));
  }, [user]);

  return (
    <AppContext.Provider value={{ user, setUser }}>
      {children}
    </AppContext.Provider>
  );
};
