export type Employee = {
  id: string;
  firstName: string;
  lastName: string;
  userName: string;
  email: string;
};

export type HomeOfficeTime = {
  id: number;
  userId?: string;
  startTime: string;
  endTime: string;
  createdAt: Date;
  updatedAt: Date | null;
};

export type Response = {
  result: any;
  isSuccess: boolean;
  message: string;
};

// export type User = {
//   id: string;
//   userName: string;
//   email: string;
// } | null;

// export type HomeOfficeTime = {
//   id: number | undefined;
//   userId: string | undefined;
//   startTime: Date | null;
//   endTime: Date | null;
// };
