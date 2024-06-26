import { CircularProgress } from "@mui/material";
import "bootstrap/dist/css/bootstrap.min.css";
import { useEffect, useState } from "react";
import { getLastSearchResults, searchBunchMoviesByTitle } from "./api/movieApi";
import "./App.css";
import IdleState from "./components/IdleState";
import MovieList from "./components/MovieList";
import MovieListHeading from "./components/MovieListHeading";
import NoData from "./components/NoData";
import SearchBox from "./components/SearchBox";
import { LastSearchResult, MoviesSearchResult } from "./utils/types";

const App = () => {
  const [loading, setLoading] = useState<boolean>(false);
  const [moviesSearchResult, setMoviesSearchResult] =
    useState<MoviesSearchResult>();
  const [searchTitle, setSearchTitle] = useState("");
  const [lastSearchResults, setLastSearchResults] =
    useState<LastSearchResult[]>();

  const loadMovies = async (title: string) => {
    setLoading(true);

    const data = await searchBunchMoviesByTitle(title);
    setMoviesSearchResult(data);

    setLoading(false);

    await loadLastSearchResults();
  };

  const loadLastSearchResults = async () => {
    const lastSearchResults = await getLastSearchResults();
    setLastSearchResults(lastSearchResults);
  };

  useEffect(() => {
    searchTitle && loadMovies(searchTitle);
    loadLastSearchResults();
  }, [searchTitle]);

  return (
    <div className="container movie-app">
      <div className="text-center">
        <MovieListHeading heading="Movies catalog" />
        <SearchBox
          searchTitle={searchTitle}
          lastSearchResults={lastSearchResults || []}
          setSearchValue={setSearchTitle}
        />
      </div>
      {loading ? (
        <div
          className="d-flex justify-content-center align-items-center"
          style={{ height: "100vh" }}
        >
          <CircularProgress size={150} />
        </div>
      ) : searchTitle ? (
        moviesSearchResult &&
        moviesSearchResult.movies &&
        moviesSearchResult.totalResults > 0 ? (
          <MovieList
            moviesSearchResult={moviesSearchResult}
            title={searchTitle}
          />
        ) : (
          <NoData title={searchTitle} />
        )
      ) : (
        <IdleState />
      )}
    </div>
  );
};

export default App;
