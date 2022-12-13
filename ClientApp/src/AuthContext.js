import { createContext } from "react";

const authContext = createContext({
  authentication: {
      authenticated: false,
      username: null
  },
  setAuthentication: (auth) => {}
});

export default authContext;