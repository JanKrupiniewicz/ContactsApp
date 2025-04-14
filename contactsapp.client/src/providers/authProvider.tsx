import { createContext, useContext, useState } from "react";
import { User, UserCredentials, UserRegistration } from "../types/types";
import { apiClient } from "../lib/apiClient";
import { register } from "module";

interface AuthContextType {
  user: User | null;
  login: (credentials: UserCredentials) => void;
  logout: () => void;
  register: (credentials: UserRegistration) => void;
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
    console.log("Login credentials:", credentials); // Log the credentials being sent

    const response = await apiClient.post("/api/login", credentials);

    console.log("Login response:", response.data); // Log the response data

    const { token, refreshToken } = response.data;

    // Store the tokens in localStorage or secure cookie for later use
    localStorage.setItem("token", token);
    localStorage.setItem("refreshToken", refreshToken);

    setUser({ email: credentials.email });
  };

  const logout = () => {
    localStorage.removeItem("accessToken");
    localStorage.removeItem("refreshToken");
    setUser(null);
  };

  const register = async (credentials: UserRegistration) => {
    const response = await apiClient.post("/api/register", credentials);
    const { token, refreshToken } = response.data;

    // Store the tokens in localStorage or secure cookie for later use
    localStorage.setItem("token", token);
    localStorage.setItem("refreshToken", refreshToken);

    setUser({ email: credentials.email });
  };

  const isAuthenticated = () => {
    return !!localStorage.getItem("accessToken");
  };

  const value = {
    user,
    login,
    logout,
    register,
    isAuthenticated,
  };

  return <AuthCtx.Provider value={value}>{children}</AuthCtx.Provider>;
};

export const useAuth = () => {
  const context = useContext(AuthCtx);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};
