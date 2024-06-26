import InfoIcon from "@mui/icons-material/Info";
import {
  Box,
  createTheme,
  IconButton,
  ImageListItem,
  ImageListItemBar,
  imageListItemClasses,
  ThemeProvider,
} from "@mui/material";
import React, { useState } from "react";
import InfiniteScroll from "react-infinite-scroll-component";
import { getMovieDetailsById, searchMoviesByTitle } from "../api/movieApi";
import { INITIAL_PAGES_COUNT, NOT_FOUND_IMAGE } from "../utils/constants";
import { Movie, MovieFullData, MoviesSearchResult } from "../utils/types";
import MovieDetails from "./MovieDetails";

const theme = createTheme({
  breakpoints: {
    values: {
      xs: 0,
      sm: 350,
      md: 650,
      lg: 900,
      xl: 1200,
    },
  },
});

const MovieList = (props: {
  moviesSearchResult: MoviesSearchResult;
  title: string;
}) => {
  const [movies, setMovies] = useState<Movie[]>(
    props.moviesSearchResult.movies
  );
  const [pageNumber, setPageNumber] = useState(INITIAL_PAGES_COUNT + 1);
  const [moreMoviesExist, setMoreMoviesExist] = useState(
    movies.length <= props.moviesSearchResult.totalResults
  );
  const [openModal, setOpenModal] = useState(false);
  const [movieDetails, setMovieDetails] = useState<MovieFullData>();
  const [movieDetailsLoading, setMovieDetailsLoading] =
    useState<boolean>(false);

  const fetchMoreMovies = async () => {
    const response = await searchMoviesByTitle(props.title, pageNumber);

    if (!response?.movies || response.movies.length === 0) {
      setMoreMoviesExist(false);
      return;
    }

    setMovies([...movies, ...response.movies]);
    setPageNumber(pageNumber + 1);
  };

  const handleOpenModalClick = async (
    e: React.MouseEvent<HTMLLIElement, MouseEvent>
  ) => {
    setMovieDetailsLoading(true);
    setOpenModal(true);

    const movieDetails = await getMovieDetailsById(e.currentTarget.id);
    setMovieDetails(movieDetails);
    setMovieDetailsLoading(false);
  };

  return (
    <ThemeProvider theme={theme}>
      <div className="my-1">
        Found ({props.moviesSearchResult.totalResults} results)
      </div>
      <InfiniteScroll
        dataLength={movies.length}
        next={fetchMoreMovies}
        hasMore={moreMoviesExist}
        loader={moreMoviesExist && <h4>Loading...</h4>}
        endMessage={
          <p style={{ textAlign: "center" }}>
            <b>Yay! You have seen it all</b>
          </p>
        }
      >
        <Box
          sx={{
            display: "grid",
            gridTemplateColumns: {
              xs: "repeat(1, 1fr)",
              sm: "repeat(2, 1fr)",
              md: "repeat(3, 1fr)",
              lg: "repeat(4, 1fr)",
              xl: "repeat(5, 1fr)",
            },
            [`& .${imageListItemClasses.root}`]: {
              display: "flex",
              flexDirection: "column",
            },
          }}
        >
          {movies.map((movie) => (
            <ImageListItem
              key={movie.imdbID}
              id={movie.imdbID}
              className="p-1 movie-item"
              onClick={(e) => handleOpenModalClick(e)}
            >
              <img
                src={`${
                  movie.poster.startsWith("N/A")
                    ? NOT_FOUND_IMAGE
                    : movie.poster + "?w=248&auto=format"
                }`}
                srcSet={`${
                  movie.poster.startsWith("N/A")
                    ? NOT_FOUND_IMAGE
                    : movie.poster + "?w=248&fit=crop&auto=format&dpr=2 2x"
                }`}
                alt={movie.title}
                loading="lazy"
              />
              <ImageListItemBar
                className="mx-1 mb-1"
                title={movie.title}
                subtitle={movie.year}
                actionIcon={
                  <IconButton
                    sx={{ color: "rgba(255, 255, 255, 0.54)" }}
                    aria-label={`info about ${movie.title}`}
                  >
                    <InfoIcon />
                  </IconButton>
                }
              />
            </ImageListItem>
          ))}
        </Box>
      </InfiniteScroll>
      {openModal && (
        <MovieDetails
          movieDetailsLoading={movieDetailsLoading}
          movieDetails={movieDetails}
          openModal={openModal}
          setOpenModal={setOpenModal}
        />
      )}
    </ThemeProvider>
  );
};

export default MovieList;
