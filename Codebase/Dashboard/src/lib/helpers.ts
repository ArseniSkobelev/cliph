import type { Cookies } from "@sveltejs/kit";

export const setAuthToken = ({
  cookies,
  token,
}: {
  cookies: Cookies;
  token: string;
}) => {
  cookies.set("AuthorizationToken", `Bearer ${token}`, {
    httpOnly: true,
    secure: true,
    sameSite: "strict",
    maxAge: 60 * 60,
    path: "/",
  });
};
