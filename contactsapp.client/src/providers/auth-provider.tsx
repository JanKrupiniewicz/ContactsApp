import { createContext, useContext, useState } from "react";
import { User, UserCredentials, UserRegistration } from "../types/types";
import { apiClient } from "../api/api-client";
import { register } from "module";

interface AuthContextType {
  user: User | null;
  login: (credentials: UserCredentials) => Promise<void>;
  logout: () => void;
  register: (credentials: UserRegistration) => Promise<void>;
  isAuthenticated: () => boolean;
}

const AuthCtx = createContext<AuthContextType | null>({
  user: null,
  login: async (credentials: UserCredentials) => {},
  logout: () => {},
  register: async (credentials: UserRegistration) => {},
  isAuthenticated: () => false,
});

export const AuthProvider = ({ children }: { children: React.ReactNode }) => {
  const [user, setUser] = useState<User | null>(null);

  const login = async (credentials: UserCredentials) => {
    const response = await apiClient.post("/Auth/login", credentials);
    const { token, refreshToken } = response.data;

    localStorage.setItem("accessToken", token);
    localStorage.setItem("refreshToken", refreshToken);

    console.log("isAuthenticated", isAuthenticated());
    console.log(localStorage.getItem("accessToken"));
    console.log(localStorage.getItem("refreshToken"));

    setUser({ email: credentials.email });
  };

  const logout = () => {
    localStorage.removeItem("accessToken");
    localStorage.removeItem("refreshToken");
    setUser(null);
  };

  const register = async (credentials: UserRegistration) => {
    const response = await apiClient.post("/Auth/register", credentials);
    const { token, refreshToken } = response.data;

    localStorage.setItem("accessToken", token);
    localStorage.setItem("refreshToken", refreshToken);

    setUser({ email: credentials.email });
  };

  const isAuthenticated = () => {
    return !!localStorage.getItem("accessToken");
  };

  return (
    <AuthCtx.Provider
      value={{ user, login, logout, register, isAuthenticated }}
    >
      {children}
    </AuthCtx.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthCtx);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};
