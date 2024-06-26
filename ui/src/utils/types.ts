export type Movie = {
  title: string;
  year: string;
  imdbID: string;
  type: string;
  poster: string;
};

export type MoviesSearchResult = {
  movies: Movie[];
  totalResults: number;
};

export interface Rating {
  source: string;
  value: string;
}

export type MovieFullData = Movie & {
  rated: string;
  released: string;
  runtime: string;
  genre: string;
  director: string;
  writer: string;
  actors: string;
  plot: string;
  language: string;
  country: string;
  awards: string;
  ratings: Rating[];
  metascore: string;
  imdbRating: string;
  imdbVotes: string;
  DVD: string;
  boxOffice: string;
  production: string;
  website: string;
};

export type LastSearchResult = {
  movieTitle: string;
  lastSearched: string;
};

export type BaseResponse<T> = {
  httpStatusCode: number;
  data: T;
  message: string;
  succeeded: boolean;
  failed: boolean;
};
