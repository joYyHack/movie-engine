import axios, { HttpStatusCode } from "axios";
import {
  BaseResponse,
  LastSearchResult,
  MovieFullData,
  MoviesSearchResult,
} from "../utils/types";
import { API_BASE_URL, INITIAL_PAGES_COUNT } from "../utils/constants";

export const searchBunchMoviesByTitle = async (title: string) => {
  return await callMovieApi<MoviesSearchResult>(
    `search-bunch-by-title?title=${title}&pages=${INITIAL_PAGES_COUNT}`
  );
};

export const searchMoviesByTitle = async (title: string, page: number) => {
  return await callMovieApi<MoviesSearchResult>(
    `search-by-title?title=${title}&page=${page}`
  );
};

export const getMovieDetailsById = async (imdbId: string) => {
  return await callMovieApi<MovieFullData>(`get-by-imdbid?IMDbId=${imdbId}`);
};

export const getLastSearchResults = async () => {
  return await callMovieApi<LastSearchResult[]>("last-search-results");
};

async function callMovieApi<T>(movieApiCall: string) {
  try {
    const { data, status } = await axios.get<BaseResponse<T>>(
      `${API_BASE_URL}/Movie/${movieApiCall}`,
      {
        headers: {
          Accept: "application/json",
        },
      }
    );

    if (status != HttpStatusCode.Ok || !data.succeeded) {
      throw new Error(data.message);
    }

    return data.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.log("error message: ", error.request.response);
    } else {
      console.log("unexpected error: ", error);
    }
    return undefined;
  }
}
