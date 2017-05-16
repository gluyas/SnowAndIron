﻿namespace Model
{
	//TODO: work in progress
	public abstract class MovementPlan
	{
		public readonly Unit Unit;
		private readonly World _world;

		private int _timeout;

		public abstract Move GetNextMove();

		// SUBCLASS HOOKS

		protected virtual void OnAccept(Move acceted)
		{

		}

		protected virtual void OnReject(Move rejected, Move replacement)
		{

		}

		// HELPER METHODS

		protected T GetLastMove<T>() where T : MovementPlan
		{
			return Unit.LastMove as T;
		}
	}
}
