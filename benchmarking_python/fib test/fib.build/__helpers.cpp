// This file contains helper functions that are automatically created from
// templates.

#include "nuitka/prelude.hpp"

extern PyObject *callPythonFunction( PyObject *func, PyObject **args, int count );


PyObject *CALL_FUNCTION_WITH_ARGS1( PyObject *called, PyObject **args )
{
    CHECK_OBJECT( called );

    // Check if arguments are valid objects in debug mode.
#ifndef __NUITKA_NO_ASSERT__
    for( size_t i = 0; i < 1; i++ )
    {
        CHECK_OBJECT( args[ i ] );
    }
#endif

    if ( Nuitka_Function_Check( called ) )
    {
        if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
        {
            return NULL;
        }

        Nuitka_FunctionObject *function = (Nuitka_FunctionObject *)called;
        PyObject *result;

        if ( function->m_args_simple && 1 == function->m_args_positional_count )
        {
            for( Py_ssize_t i = 0; i < 1; i++ )
            {
                Py_INCREF( args[ i ] );
            }

            result = function->m_c_code( function, args );
        }
        else if ( function->m_args_simple && 1 + function->m_defaults_given == function->m_args_positional_count )
        {
#ifdef _MSC_VER
            PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_positional_count );
#else
            PyObject *python_pars[ function->m_args_positional_count ];
#endif
            memcpy( python_pars, args, 1 * sizeof(PyObject *) );
            memcpy( python_pars + 1, &PyTuple_GET_ITEM( function->m_defaults, 0 ), function->m_defaults_given * sizeof(PyObject *) );

            for( Py_ssize_t i = 0; i < function->m_args_positional_count; i++ )
            {
                Py_INCREF( python_pars[ i ] );
            }

            result = function->m_c_code( function, python_pars );
        }
        else
        {
#ifdef _MSC_VER
            PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_overall_count );
#else
            PyObject *python_pars[ function->m_args_overall_count ];
#endif
            memset( python_pars, 0, function->m_args_overall_count * sizeof(PyObject *) );

            if ( parseArgumentsPos( function, python_pars, args, 1 ))
            {
                result = function->m_c_code( function, python_pars );
            }
            else
            {
                result = NULL;
            }
        }

        Py_LeaveRecursiveCall();

        return result;
    }
    else if ( Nuitka_Method_Check( called ) )
    {
        Nuitka_MethodObject *method = (Nuitka_MethodObject *)called;

        // Unbound method without arguments, let the error path be slow.
        if ( method->m_object != NULL )
        {
            if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
            {
                return NULL;
            }

            Nuitka_FunctionObject *function = method->m_function;

            PyObject *result;

            if ( function->m_args_simple && 1 + 1 == function->m_args_positional_count )
            {
#ifdef _MSC_VER
                PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_positional_count );
#else
                PyObject *python_pars[ function->m_args_positional_count ];
#endif
                python_pars[ 0 ] = method->m_object;
                Py_INCREF( method->m_object );

                for( Py_ssize_t i = 0; i < 1; i++ )
                {
                    python_pars[ i + 1 ] = args[ i ];
                    Py_INCREF( args[ i ] );
                }

                result = function->m_c_code( function, python_pars );
            }
            else if ( function->m_args_simple && 1 + 1 + function->m_defaults_given == function->m_args_positional_count )
            {
#ifdef _MSC_VER
                PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_positional_count );
#else
                PyObject *python_pars[ function->m_args_positional_count ];
#endif
                python_pars[ 0 ] = method->m_object;
                Py_INCREF( method->m_object );

                memcpy( python_pars+1, args, 1 * sizeof(PyObject *) );
                memcpy( python_pars+1 + 1, &PyTuple_GET_ITEM( function->m_defaults, 0 ), function->m_defaults_given * sizeof(PyObject *) );

                for( Py_ssize_t i = 1; i < function->m_args_overall_count; i++ )
                {
                    Py_INCREF( python_pars[ i ] );
                }

                result = function->m_c_code( function, python_pars );
            }
            else
            {
#ifdef _MSC_VER
                PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_overall_count );
#else
                PyObject *python_pars[ function->m_args_overall_count ];
#endif
                memset( python_pars, 0, function->m_args_overall_count * sizeof(PyObject *) );

                if ( parseArgumentsMethodPos( function, python_pars, method->m_object, args, 1 ) )
                {
                    result = function->m_c_code( function, python_pars );
                }
                else
                {
                    result = NULL;
                }
            }

            Py_LeaveRecursiveCall();

            return result;
        }
    }
    else if ( PyCFunction_Check( called ) )
    {
        // Try to be fast about wrapping the arguments.
        int flags = PyCFunction_GET_FLAGS( called );

        if ( flags & METH_NOARGS )
        {
#if 1 == 0
            PyCFunction method = PyCFunction_GET_FUNCTION( called );
            PyObject *self = PyCFunction_GET_SELF( called );

            // Recursion guard is not strictly necessary, as we already have
            // one on our way to here.
#ifdef _NUITKA_FULL_COMPAT
            if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
            {
                return NULL;
            }
#endif

            PyObject *result = (*method)( self, NULL );

#ifdef _NUITKA_FULL_COMPAT
            Py_LeaveRecursiveCall();
#endif

            if ( result != NULL )
            {
            // Some buggy C functions do set an error, but do not indicate it
            // and Nuitka inner workings can get upset/confused from it.
                DROP_ERROR_OCCURRED();

                return result;
            }
            else
            {
                // Other buggy C functions do this, return NULL, but with
                // no error set, not allowed.
                if (unlikely( !ERROR_OCCURRED() ))
                {
                    PyErr_Format(
                        PyExc_SystemError,
                        "NULL result without error in PyObject_Call"
                    );
                }

                return NULL;
            }
#else
            PyErr_Format(
                PyExc_TypeError,
                "%s() takes no arguments (1 given)",
                ((PyCFunctionObject *)called)->m_ml->ml_name
            );
            return NULL;
#endif
        }
        else if ( flags & METH_O )
        {
#if 1 == 1
            PyCFunction method = PyCFunction_GET_FUNCTION( called );
            PyObject *self = PyCFunction_GET_SELF( called );

            // Recursion guard is not strictly necessary, as we already have
            // one on our way to here.
#ifdef _NUITKA_FULL_COMPAT
            if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
            {
                return NULL;
            }
#endif

            PyObject *result = (*method)( self, args[0] );

#ifdef _NUITKA_FULL_COMPAT
            Py_LeaveRecursiveCall();
#endif

            if ( result != NULL )
            {
            // Some buggy C functions do set an error, but do not indicate it
            // and Nuitka inner workings can get upset/confused from it.
                DROP_ERROR_OCCURRED();

                return result;
            }
            else
            {
                // Other buggy C functions do this, return NULL, but with
                // no error set, not allowed.
                if (unlikely( !ERROR_OCCURRED() ))
                {
                    PyErr_Format(
                        PyExc_SystemError,
                        "NULL result without error in PyObject_Call"
                    );
                }

                return NULL;
            }
#else
            PyErr_Format(PyExc_TypeError,
                "%s() takes exactly one argument (1 given)",
                 ((PyCFunctionObject *)called)->m_ml->ml_name
            );
            return NULL;
#endif
        }
        else
        {
            PyCFunction method = PyCFunction_GET_FUNCTION( called );
            PyObject *self = PyCFunction_GET_SELF( called );

            PyObject *pos_args = MAKE_TUPLE( args, 1 );

            PyObject *result;

            assert( flags && METH_VARARGS );

            // Recursion guard is not strictly necessary, as we already have
            // one on our way to here.
#ifdef _NUITKA_FULL_COMPAT
            if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
            {
                return NULL;
            }
#endif

            if ( flags && METH_KEYWORDS )
            {
                result = (*(PyCFunctionWithKeywords)method)( self, pos_args, NULL );
            }
            else
            {
                result = (*method)( self, pos_args );
            }

#ifdef _NUITKA_FULL_COMPAT
            Py_LeaveRecursiveCall();
#endif

            if ( result != NULL )
            {
            // Some buggy C functions do set an error, but do not indicate it
            // and Nuitka inner workings can get upset/confused from it.
                DROP_ERROR_OCCURRED();

                Py_DECREF( pos_args );
                return result;
            }
            else
            {
                // Other buggy C functions do this, return NULL, but with
                // no error set, not allowed.
                if (unlikely( !ERROR_OCCURRED() ))
                {
                    PyErr_Format(
                        PyExc_SystemError,
                        "NULL result without error in PyObject_Call"
                    );
                }

                Py_DECREF( pos_args );
                return NULL;
            }
        }
    }
    else if ( PyFunction_Check( called ) )
    {
        return callPythonFunction(
            called,
            args,
            1
        );
    }

    PyObject *pos_args = MAKE_TUPLE( args, 1 );

    PyObject *result = CALL_FUNCTION(
        called,
        pos_args,
        NULL
    );

    Py_DECREF( pos_args );

    return result;
}

PyObject *CALL_FUNCTION_WITH_ARGS2( PyObject *called, PyObject **args )
{
    CHECK_OBJECT( called );

    // Check if arguments are valid objects in debug mode.
#ifndef __NUITKA_NO_ASSERT__
    for( size_t i = 0; i < 2; i++ )
    {
        CHECK_OBJECT( args[ i ] );
    }
#endif

    if ( Nuitka_Function_Check( called ) )
    {
        if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
        {
            return NULL;
        }

        Nuitka_FunctionObject *function = (Nuitka_FunctionObject *)called;
        PyObject *result;

        if ( function->m_args_simple && 2 == function->m_args_positional_count )
        {
            for( Py_ssize_t i = 0; i < 2; i++ )
            {
                Py_INCREF( args[ i ] );
            }

            result = function->m_c_code( function, args );
        }
        else if ( function->m_args_simple && 2 + function->m_defaults_given == function->m_args_positional_count )
        {
#ifdef _MSC_VER
            PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_positional_count );
#else
            PyObject *python_pars[ function->m_args_positional_count ];
#endif
            memcpy( python_pars, args, 2 * sizeof(PyObject *) );
            memcpy( python_pars + 2, &PyTuple_GET_ITEM( function->m_defaults, 0 ), function->m_defaults_given * sizeof(PyObject *) );

            for( Py_ssize_t i = 0; i < function->m_args_positional_count; i++ )
            {
                Py_INCREF( python_pars[ i ] );
            }

            result = function->m_c_code( function, python_pars );
        }
        else
        {
#ifdef _MSC_VER
            PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_overall_count );
#else
            PyObject *python_pars[ function->m_args_overall_count ];
#endif
            memset( python_pars, 0, function->m_args_overall_count * sizeof(PyObject *) );

            if ( parseArgumentsPos( function, python_pars, args, 2 ))
            {
                result = function->m_c_code( function, python_pars );
            }
            else
            {
                result = NULL;
            }
        }

        Py_LeaveRecursiveCall();

        return result;
    }
    else if ( Nuitka_Method_Check( called ) )
    {
        Nuitka_MethodObject *method = (Nuitka_MethodObject *)called;

        // Unbound method without arguments, let the error path be slow.
        if ( method->m_object != NULL )
        {
            if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
            {
                return NULL;
            }

            Nuitka_FunctionObject *function = method->m_function;

            PyObject *result;

            if ( function->m_args_simple && 2 + 1 == function->m_args_positional_count )
            {
#ifdef _MSC_VER
                PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_positional_count );
#else
                PyObject *python_pars[ function->m_args_positional_count ];
#endif
                python_pars[ 0 ] = method->m_object;
                Py_INCREF( method->m_object );

                for( Py_ssize_t i = 0; i < 2; i++ )
                {
                    python_pars[ i + 1 ] = args[ i ];
                    Py_INCREF( args[ i ] );
                }

                result = function->m_c_code( function, python_pars );
            }
            else if ( function->m_args_simple && 2 + 1 + function->m_defaults_given == function->m_args_positional_count )
            {
#ifdef _MSC_VER
                PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_positional_count );
#else
                PyObject *python_pars[ function->m_args_positional_count ];
#endif
                python_pars[ 0 ] = method->m_object;
                Py_INCREF( method->m_object );

                memcpy( python_pars+1, args, 2 * sizeof(PyObject *) );
                memcpy( python_pars+1 + 2, &PyTuple_GET_ITEM( function->m_defaults, 0 ), function->m_defaults_given * sizeof(PyObject *) );

                for( Py_ssize_t i = 1; i < function->m_args_overall_count; i++ )
                {
                    Py_INCREF( python_pars[ i ] );
                }

                result = function->m_c_code( function, python_pars );
            }
            else
            {
#ifdef _MSC_VER
                PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_overall_count );
#else
                PyObject *python_pars[ function->m_args_overall_count ];
#endif
                memset( python_pars, 0, function->m_args_overall_count * sizeof(PyObject *) );

                if ( parseArgumentsMethodPos( function, python_pars, method->m_object, args, 2 ) )
                {
                    result = function->m_c_code( function, python_pars );
                }
                else
                {
                    result = NULL;
                }
            }

            Py_LeaveRecursiveCall();

            return result;
        }
    }
    else if ( PyCFunction_Check( called ) )
    {
        // Try to be fast about wrapping the arguments.
        int flags = PyCFunction_GET_FLAGS( called );

        if ( flags & METH_NOARGS )
        {
#if 2 == 0
            PyCFunction method = PyCFunction_GET_FUNCTION( called );
            PyObject *self = PyCFunction_GET_SELF( called );

            // Recursion guard is not strictly necessary, as we already have
            // one on our way to here.
#ifdef _NUITKA_FULL_COMPAT
            if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
            {
                return NULL;
            }
#endif

            PyObject *result = (*method)( self, NULL );

#ifdef _NUITKA_FULL_COMPAT
            Py_LeaveRecursiveCall();
#endif

            if ( result != NULL )
            {
            // Some buggy C functions do set an error, but do not indicate it
            // and Nuitka inner workings can get upset/confused from it.
                DROP_ERROR_OCCURRED();

                return result;
            }
            else
            {
                // Other buggy C functions do this, return NULL, but with
                // no error set, not allowed.
                if (unlikely( !ERROR_OCCURRED() ))
                {
                    PyErr_Format(
                        PyExc_SystemError,
                        "NULL result without error in PyObject_Call"
                    );
                }

                return NULL;
            }
#else
            PyErr_Format(
                PyExc_TypeError,
                "%s() takes no arguments (2 given)",
                ((PyCFunctionObject *)called)->m_ml->ml_name
            );
            return NULL;
#endif
        }
        else if ( flags & METH_O )
        {
#if 2 == 1
            PyCFunction method = PyCFunction_GET_FUNCTION( called );
            PyObject *self = PyCFunction_GET_SELF( called );

            // Recursion guard is not strictly necessary, as we already have
            // one on our way to here.
#ifdef _NUITKA_FULL_COMPAT
            if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
            {
                return NULL;
            }
#endif

            PyObject *result = (*method)( self, args[0] );

#ifdef _NUITKA_FULL_COMPAT
            Py_LeaveRecursiveCall();
#endif

            if ( result != NULL )
            {
            // Some buggy C functions do set an error, but do not indicate it
            // and Nuitka inner workings can get upset/confused from it.
                DROP_ERROR_OCCURRED();

                return result;
            }
            else
            {
                // Other buggy C functions do this, return NULL, but with
                // no error set, not allowed.
                if (unlikely( !ERROR_OCCURRED() ))
                {
                    PyErr_Format(
                        PyExc_SystemError,
                        "NULL result without error in PyObject_Call"
                    );
                }

                return NULL;
            }
#else
            PyErr_Format(PyExc_TypeError,
                "%s() takes exactly one argument (2 given)",
                 ((PyCFunctionObject *)called)->m_ml->ml_name
            );
            return NULL;
#endif
        }
        else
        {
            PyCFunction method = PyCFunction_GET_FUNCTION( called );
            PyObject *self = PyCFunction_GET_SELF( called );

            PyObject *pos_args = MAKE_TUPLE( args, 2 );

            PyObject *result;

            assert( flags && METH_VARARGS );

            // Recursion guard is not strictly necessary, as we already have
            // one on our way to here.
#ifdef _NUITKA_FULL_COMPAT
            if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
            {
                return NULL;
            }
#endif

            if ( flags && METH_KEYWORDS )
            {
                result = (*(PyCFunctionWithKeywords)method)( self, pos_args, NULL );
            }
            else
            {
                result = (*method)( self, pos_args );
            }

#ifdef _NUITKA_FULL_COMPAT
            Py_LeaveRecursiveCall();
#endif

            if ( result != NULL )
            {
            // Some buggy C functions do set an error, but do not indicate it
            // and Nuitka inner workings can get upset/confused from it.
                DROP_ERROR_OCCURRED();

                Py_DECREF( pos_args );
                return result;
            }
            else
            {
                // Other buggy C functions do this, return NULL, but with
                // no error set, not allowed.
                if (unlikely( !ERROR_OCCURRED() ))
                {
                    PyErr_Format(
                        PyExc_SystemError,
                        "NULL result without error in PyObject_Call"
                    );
                }

                Py_DECREF( pos_args );
                return NULL;
            }
        }
    }
    else if ( PyFunction_Check( called ) )
    {
        return callPythonFunction(
            called,
            args,
            2
        );
    }

    PyObject *pos_args = MAKE_TUPLE( args, 2 );

    PyObject *result = CALL_FUNCTION(
        called,
        pos_args,
        NULL
    );

    Py_DECREF( pos_args );

    return result;
}

PyObject *CALL_FUNCTION_WITH_ARGS3( PyObject *called, PyObject **args )
{
    CHECK_OBJECT( called );

    // Check if arguments are valid objects in debug mode.
#ifndef __NUITKA_NO_ASSERT__
    for( size_t i = 0; i < 3; i++ )
    {
        CHECK_OBJECT( args[ i ] );
    }
#endif

    if ( Nuitka_Function_Check( called ) )
    {
        if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
        {
            return NULL;
        }

        Nuitka_FunctionObject *function = (Nuitka_FunctionObject *)called;
        PyObject *result;

        if ( function->m_args_simple && 3 == function->m_args_positional_count )
        {
            for( Py_ssize_t i = 0; i < 3; i++ )
            {
                Py_INCREF( args[ i ] );
            }

            result = function->m_c_code( function, args );
        }
        else if ( function->m_args_simple && 3 + function->m_defaults_given == function->m_args_positional_count )
        {
#ifdef _MSC_VER
            PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_positional_count );
#else
            PyObject *python_pars[ function->m_args_positional_count ];
#endif
            memcpy( python_pars, args, 3 * sizeof(PyObject *) );
            memcpy( python_pars + 3, &PyTuple_GET_ITEM( function->m_defaults, 0 ), function->m_defaults_given * sizeof(PyObject *) );

            for( Py_ssize_t i = 0; i < function->m_args_positional_count; i++ )
            {
                Py_INCREF( python_pars[ i ] );
            }

            result = function->m_c_code( function, python_pars );
        }
        else
        {
#ifdef _MSC_VER
            PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_overall_count );
#else
            PyObject *python_pars[ function->m_args_overall_count ];
#endif
            memset( python_pars, 0, function->m_args_overall_count * sizeof(PyObject *) );

            if ( parseArgumentsPos( function, python_pars, args, 3 ))
            {
                result = function->m_c_code( function, python_pars );
            }
            else
            {
                result = NULL;
            }
        }

        Py_LeaveRecursiveCall();

        return result;
    }
    else if ( Nuitka_Method_Check( called ) )
    {
        Nuitka_MethodObject *method = (Nuitka_MethodObject *)called;

        // Unbound method without arguments, let the error path be slow.
        if ( method->m_object != NULL )
        {
            if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
            {
                return NULL;
            }

            Nuitka_FunctionObject *function = method->m_function;

            PyObject *result;

            if ( function->m_args_simple && 3 + 1 == function->m_args_positional_count )
            {
#ifdef _MSC_VER
                PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_positional_count );
#else
                PyObject *python_pars[ function->m_args_positional_count ];
#endif
                python_pars[ 0 ] = method->m_object;
                Py_INCREF( method->m_object );

                for( Py_ssize_t i = 0; i < 3; i++ )
                {
                    python_pars[ i + 1 ] = args[ i ];
                    Py_INCREF( args[ i ] );
                }

                result = function->m_c_code( function, python_pars );
            }
            else if ( function->m_args_simple && 3 + 1 + function->m_defaults_given == function->m_args_positional_count )
            {
#ifdef _MSC_VER
                PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_positional_count );
#else
                PyObject *python_pars[ function->m_args_positional_count ];
#endif
                python_pars[ 0 ] = method->m_object;
                Py_INCREF( method->m_object );

                memcpy( python_pars+1, args, 3 * sizeof(PyObject *) );
                memcpy( python_pars+1 + 3, &PyTuple_GET_ITEM( function->m_defaults, 0 ), function->m_defaults_given * sizeof(PyObject *) );

                for( Py_ssize_t i = 1; i < function->m_args_overall_count; i++ )
                {
                    Py_INCREF( python_pars[ i ] );
                }

                result = function->m_c_code( function, python_pars );
            }
            else
            {
#ifdef _MSC_VER
                PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_overall_count );
#else
                PyObject *python_pars[ function->m_args_overall_count ];
#endif
                memset( python_pars, 0, function->m_args_overall_count * sizeof(PyObject *) );

                if ( parseArgumentsMethodPos( function, python_pars, method->m_object, args, 3 ) )
                {
                    result = function->m_c_code( function, python_pars );
                }
                else
                {
                    result = NULL;
                }
            }

            Py_LeaveRecursiveCall();

            return result;
        }
    }
    else if ( PyCFunction_Check( called ) )
    {
        // Try to be fast about wrapping the arguments.
        int flags = PyCFunction_GET_FLAGS( called );

        if ( flags & METH_NOARGS )
        {
#if 3 == 0
            PyCFunction method = PyCFunction_GET_FUNCTION( called );
            PyObject *self = PyCFunction_GET_SELF( called );

            // Recursion guard is not strictly necessary, as we already have
            // one on our way to here.
#ifdef _NUITKA_FULL_COMPAT
            if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
            {
                return NULL;
            }
#endif

            PyObject *result = (*method)( self, NULL );

#ifdef _NUITKA_FULL_COMPAT
            Py_LeaveRecursiveCall();
#endif

            if ( result != NULL )
            {
            // Some buggy C functions do set an error, but do not indicate it
            // and Nuitka inner workings can get upset/confused from it.
                DROP_ERROR_OCCURRED();

                return result;
            }
            else
            {
                // Other buggy C functions do this, return NULL, but with
                // no error set, not allowed.
                if (unlikely( !ERROR_OCCURRED() ))
                {
                    PyErr_Format(
                        PyExc_SystemError,
                        "NULL result without error in PyObject_Call"
                    );
                }

                return NULL;
            }
#else
            PyErr_Format(
                PyExc_TypeError,
                "%s() takes no arguments (3 given)",
                ((PyCFunctionObject *)called)->m_ml->ml_name
            );
            return NULL;
#endif
        }
        else if ( flags & METH_O )
        {
#if 3 == 1
            PyCFunction method = PyCFunction_GET_FUNCTION( called );
            PyObject *self = PyCFunction_GET_SELF( called );

            // Recursion guard is not strictly necessary, as we already have
            // one on our way to here.
#ifdef _NUITKA_FULL_COMPAT
            if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
            {
                return NULL;
            }
#endif

            PyObject *result = (*method)( self, args[0] );

#ifdef _NUITKA_FULL_COMPAT
            Py_LeaveRecursiveCall();
#endif

            if ( result != NULL )
            {
            // Some buggy C functions do set an error, but do not indicate it
            // and Nuitka inner workings can get upset/confused from it.
                DROP_ERROR_OCCURRED();

                return result;
            }
            else
            {
                // Other buggy C functions do this, return NULL, but with
                // no error set, not allowed.
                if (unlikely( !ERROR_OCCURRED() ))
                {
                    PyErr_Format(
                        PyExc_SystemError,
                        "NULL result without error in PyObject_Call"
                    );
                }

                return NULL;
            }
#else
            PyErr_Format(PyExc_TypeError,
                "%s() takes exactly one argument (3 given)",
                 ((PyCFunctionObject *)called)->m_ml->ml_name
            );
            return NULL;
#endif
        }
        else
        {
            PyCFunction method = PyCFunction_GET_FUNCTION( called );
            PyObject *self = PyCFunction_GET_SELF( called );

            PyObject *pos_args = MAKE_TUPLE( args, 3 );

            PyObject *result;

            assert( flags && METH_VARARGS );

            // Recursion guard is not strictly necessary, as we already have
            // one on our way to here.
#ifdef _NUITKA_FULL_COMPAT
            if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
            {
                return NULL;
            }
#endif

            if ( flags && METH_KEYWORDS )
            {
                result = (*(PyCFunctionWithKeywords)method)( self, pos_args, NULL );
            }
            else
            {
                result = (*method)( self, pos_args );
            }

#ifdef _NUITKA_FULL_COMPAT
            Py_LeaveRecursiveCall();
#endif

            if ( result != NULL )
            {
            // Some buggy C functions do set an error, but do not indicate it
            // and Nuitka inner workings can get upset/confused from it.
                DROP_ERROR_OCCURRED();

                Py_DECREF( pos_args );
                return result;
            }
            else
            {
                // Other buggy C functions do this, return NULL, but with
                // no error set, not allowed.
                if (unlikely( !ERROR_OCCURRED() ))
                {
                    PyErr_Format(
                        PyExc_SystemError,
                        "NULL result without error in PyObject_Call"
                    );
                }

                Py_DECREF( pos_args );
                return NULL;
            }
        }
    }
    else if ( PyFunction_Check( called ) )
    {
        return callPythonFunction(
            called,
            args,
            3
        );
    }

    PyObject *pos_args = MAKE_TUPLE( args, 3 );

    PyObject *result = CALL_FUNCTION(
        called,
        pos_args,
        NULL
    );

    Py_DECREF( pos_args );

    return result;
}
/* Code to register embedded modules for meta path based loading if any. */

#include "nuitka/unfreezing.hpp"

/* Table for lookup to find compiled or bytecode modules included in this
 * binary or module, or put along this binary as extension modules. We do
 * our own loading for each of these.
 */
MOD_INIT_DECL( __main__ );
static struct Nuitka_MetaPathBasedLoaderEntry meta_path_loader_entries[] =
{
    { (char *)"__main__", MOD_INIT_NAME( __main__ ), NULL, 0, NUITKA_COMPILED_MODULE },
    { (char *)"_bsddb", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_codecs_cn", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_codecs_hk", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_codecs_iso2022", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_codecs_jp", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_codecs_kr", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_codecs_tw", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_csv", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_ctypes", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_curses", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_curses_panel", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_elementtree", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_hashlib", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_hotshot", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_json", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_lsprof", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_multibytecodec", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_multiprocessing", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_sqlite3", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_ssl", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"bz2", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"dbm", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"mmap", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"parser", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"pyexpat", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"readline", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"resource", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"termios", NULL, NULL, 0, NUITKA_SHLIB_FLAG },
    { (char *)"BaseHTTPServer", NULL, &constant_bin[ 300 ], 21682, NUITKA_BYTECODE_FLAG },
    { (char *)"Bastion", NULL, &constant_bin[ 21982 ], 6622, NUITKA_BYTECODE_FLAG },
    { (char *)"CGIHTTPServer", NULL, &constant_bin[ 28604 ], 10984, NUITKA_BYTECODE_FLAG },
    { (char *)"Canvas", NULL, &constant_bin[ 39588 ], 15396, NUITKA_BYTECODE_FLAG },
    { (char *)"ConfigParser", NULL, &constant_bin[ 54984 ], 25087, NUITKA_BYTECODE_FLAG },
    { (char *)"Cookie", NULL, &constant_bin[ 80071 ], 22574, NUITKA_BYTECODE_FLAG },
    { (char *)"Dialog", NULL, &constant_bin[ 102645 ], 1902, NUITKA_BYTECODE_FLAG },
    { (char *)"DocXMLRPCServer", NULL, &constant_bin[ 104547 ], 9791, NUITKA_BYTECODE_FLAG },
    { (char *)"FileDialog", NULL, &constant_bin[ 114338 ], 9672, NUITKA_BYTECODE_FLAG },
    { (char *)"FixTk", NULL, &constant_bin[ 124010 ], 2078, NUITKA_BYTECODE_FLAG },
    { (char *)"HTMLParser", NULL, &constant_bin[ 126088 ], 13656, NUITKA_BYTECODE_FLAG },
    { (char *)"MimeWriter", NULL, &constant_bin[ 139744 ], 7338, NUITKA_BYTECODE_FLAG },
    { (char *)"Queue", NULL, &constant_bin[ 147082 ], 9360, NUITKA_BYTECODE_FLAG },
    { (char *)"ScrolledText", NULL, &constant_bin[ 156442 ], 2646, NUITKA_BYTECODE_FLAG },
    { (char *)"SimpleDialog", NULL, &constant_bin[ 159088 ], 4309, NUITKA_BYTECODE_FLAG },
    { (char *)"SimpleHTTPServer", NULL, &constant_bin[ 163397 ], 7980, NUITKA_BYTECODE_FLAG },
    { (char *)"SimpleXMLRPCServer", NULL, &constant_bin[ 171377 ], 22782, NUITKA_BYTECODE_FLAG },
    { (char *)"SocketServer", NULL, &constant_bin[ 194159 ], 23948, NUITKA_BYTECODE_FLAG },
    { (char *)"StringIO", NULL, &constant_bin[ 218107 ], 11434, NUITKA_BYTECODE_FLAG },
    { (char *)"Tix", NULL, &constant_bin[ 229541 ], 95379, NUITKA_BYTECODE_FLAG },
    { (char *)"Tkconstants", NULL, &constant_bin[ 324920 ], 2236, NUITKA_BYTECODE_FLAG },
    { (char *)"Tkdnd", NULL, &constant_bin[ 327156 ], 12765, NUITKA_BYTECODE_FLAG },
    { (char *)"Tkinter", NULL, &constant_bin[ 339921 ], 199053, NUITKA_BYTECODE_FLAG },
    { (char *)"UserDict", NULL, &constant_bin[ 538974 ], 9613, NUITKA_BYTECODE_FLAG },
    { (char *)"UserList", NULL, &constant_bin[ 548587 ], 6501, NUITKA_BYTECODE_FLAG },
    { (char *)"UserString", NULL, &constant_bin[ 555088 ], 14720, NUITKA_BYTECODE_FLAG },
    { (char *)"_LWPCookieJar", NULL, &constant_bin[ 569808 ], 5513, NUITKA_BYTECODE_FLAG },
    { (char *)"_MozillaCookieJar", NULL, &constant_bin[ 575321 ], 4445, NUITKA_BYTECODE_FLAG },
    { (char *)"__future__", NULL, &constant_bin[ 579766 ], 4272, NUITKA_BYTECODE_FLAG },
    { (char *)"_abcoll", NULL, &constant_bin[ 584038 ], 25466, NUITKA_BYTECODE_FLAG },
    { (char *)"_osx_support", NULL, &constant_bin[ 609504 ], 11752, NUITKA_BYTECODE_FLAG },
    { (char *)"_pyio", NULL, &constant_bin[ 621256 ], 64318, NUITKA_BYTECODE_FLAG },
    { (char *)"_strptime", NULL, &constant_bin[ 685574 ], 15121, NUITKA_BYTECODE_FLAG },
    { (char *)"_sysconfigdata", NULL, &constant_bin[ 700695 ], 271, NUITKA_BYTECODE_FLAG },
    { (char *)"_sysconfigdata_nd", NULL, &constant_bin[ 700966 ], 20800, NUITKA_BYTECODE_FLAG },
    { (char *)"_threading_local", NULL, &constant_bin[ 721766 ], 6578, NUITKA_BYTECODE_FLAG },
    { (char *)"_weakrefset", NULL, &constant_bin[ 728344 ], 9574, NUITKA_BYTECODE_FLAG },
    { (char *)"abc", NULL, &constant_bin[ 737918 ], 6113, NUITKA_BYTECODE_FLAG },
    { (char *)"aifc", NULL, &constant_bin[ 744031 ], 30327, NUITKA_BYTECODE_FLAG },
    { (char *)"anydbm", NULL, &constant_bin[ 774358 ], 2786, NUITKA_BYTECODE_FLAG },
    { (char *)"argparse", NULL, &constant_bin[ 777144 ], 64033, NUITKA_BYTECODE_FLAG },
    { (char *)"ast", NULL, &constant_bin[ 841177 ], 12898, NUITKA_BYTECODE_FLAG },
    { (char *)"asynchat", NULL, &constant_bin[ 854075 ], 8751, NUITKA_BYTECODE_FLAG },
    { (char *)"asyncore", NULL, &constant_bin[ 862826 ], 18714, NUITKA_BYTECODE_FLAG },
    { (char *)"atexit", NULL, &constant_bin[ 881540 ], 2183, NUITKA_BYTECODE_FLAG },
    { (char *)"audiodev", NULL, &constant_bin[ 883723 ], 8407, NUITKA_BYTECODE_FLAG },
    { (char *)"bdb", NULL, &constant_bin[ 892130 ], 18971, NUITKA_BYTECODE_FLAG },
    { (char *)"binhex", NULL, &constant_bin[ 911101 ], 15349, NUITKA_BYTECODE_FLAG },
    { (char *)"bisect", NULL, &constant_bin[ 926450 ], 3053, NUITKA_BYTECODE_FLAG },
    { (char *)"bsddb", NULL, &constant_bin[ 929503 ], 12368, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"bsddb.db", NULL, &constant_bin[ 941871 ], 582, NUITKA_BYTECODE_FLAG },
    { (char *)"bsddb.dbobj", NULL, &constant_bin[ 942453 ], 18601, NUITKA_BYTECODE_FLAG },
    { (char *)"bsddb.dbrecio", NULL, &constant_bin[ 961054 ], 5269, NUITKA_BYTECODE_FLAG },
    { (char *)"bsddb.dbshelve", NULL, &constant_bin[ 966323 ], 12910, NUITKA_BYTECODE_FLAG },
    { (char *)"bsddb.dbtables", NULL, &constant_bin[ 979233 ], 24402, NUITKA_BYTECODE_FLAG },
    { (char *)"bsddb.dbutils", NULL, &constant_bin[ 1003635 ], 1615, NUITKA_BYTECODE_FLAG },
    { (char *)"cProfile", NULL, &constant_bin[ 1005250 ], 6272, NUITKA_BYTECODE_FLAG },
    { (char *)"calendar", NULL, &constant_bin[ 1011522 ], 27624, NUITKA_BYTECODE_FLAG },
    { (char *)"cgi", NULL, &constant_bin[ 1039146 ], 32417, NUITKA_BYTECODE_FLAG },
    { (char *)"cgitb", NULL, &constant_bin[ 1071563 ], 12140, NUITKA_BYTECODE_FLAG },
    { (char *)"chunk", NULL, &constant_bin[ 1083703 ], 5571, NUITKA_BYTECODE_FLAG },
    { (char *)"cmd", NULL, &constant_bin[ 1089274 ], 13989, NUITKA_BYTECODE_FLAG },
    { (char *)"code", NULL, &constant_bin[ 1103263 ], 10294, NUITKA_BYTECODE_FLAG },
    { (char *)"codeop", NULL, &constant_bin[ 1113557 ], 6569, NUITKA_BYTECODE_FLAG },
    { (char *)"collections", NULL, &constant_bin[ 1120126 ], 26051, NUITKA_BYTECODE_FLAG },
    { (char *)"colorsys", NULL, &constant_bin[ 1146177 ], 3967, NUITKA_BYTECODE_FLAG },
    { (char *)"commands", NULL, &constant_bin[ 1150144 ], 2449, NUITKA_BYTECODE_FLAG },
    { (char *)"compileall", NULL, &constant_bin[ 1152593 ], 6997, NUITKA_BYTECODE_FLAG },
    { (char *)"compiler", NULL, &constant_bin[ 1159590 ], 1287, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"compiler.ast", NULL, &constant_bin[ 1160877 ], 71339, NUITKA_BYTECODE_FLAG },
    { (char *)"compiler.consts", NULL, &constant_bin[ 1232216 ], 727, NUITKA_BYTECODE_FLAG },
    { (char *)"compiler.future", NULL, &constant_bin[ 1232943 ], 3018, NUITKA_BYTECODE_FLAG },
    { (char *)"compiler.misc", NULL, &constant_bin[ 1235961 ], 3687, NUITKA_BYTECODE_FLAG },
    { (char *)"compiler.pyassem", NULL, &constant_bin[ 1239648 ], 25831, NUITKA_BYTECODE_FLAG },
    { (char *)"compiler.pycodegen", NULL, &constant_bin[ 1265479 ], 56161, NUITKA_BYTECODE_FLAG },
    { (char *)"compiler.symbols", NULL, &constant_bin[ 1321640 ], 17557, NUITKA_BYTECODE_FLAG },
    { (char *)"compiler.syntax", NULL, &constant_bin[ 1339197 ], 1862, NUITKA_BYTECODE_FLAG },
    { (char *)"compiler.transformer", NULL, &constant_bin[ 1341059 ], 47372, NUITKA_BYTECODE_FLAG },
    { (char *)"compiler.visitor", NULL, &constant_bin[ 1388431 ], 4159, NUITKA_BYTECODE_FLAG },
    { (char *)"contextlib", NULL, &constant_bin[ 1392590 ], 4422, NUITKA_BYTECODE_FLAG },
    { (char *)"cookielib", NULL, &constant_bin[ 1397012 ], 54567, NUITKA_BYTECODE_FLAG },
    { (char *)"copy", NULL, &constant_bin[ 1451579 ], 12140, NUITKA_BYTECODE_FLAG },
    { (char *)"csv", NULL, &constant_bin[ 1463719 ], 13454, NUITKA_BYTECODE_FLAG },
    { (char *)"ctypes", NULL, &constant_bin[ 1477173 ], 20192, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"ctypes._endian", NULL, &constant_bin[ 1497365 ], 2287, NUITKA_BYTECODE_FLAG },
    { (char *)"ctypes.util", NULL, &constant_bin[ 1499652 ], 8518, NUITKA_BYTECODE_FLAG },
    { (char *)"curses", NULL, &constant_bin[ 1508170 ], 1539, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"curses.ascii", NULL, &constant_bin[ 1509709 ], 5040, NUITKA_BYTECODE_FLAG },
    { (char *)"curses.has_key", NULL, &constant_bin[ 1514749 ], 5925, NUITKA_BYTECODE_FLAG },
    { (char *)"curses.panel", NULL, &constant_bin[ 1520674 ], 267, NUITKA_BYTECODE_FLAG },
    { (char *)"curses.textpad", NULL, &constant_bin[ 1520941 ], 6842, NUITKA_BYTECODE_FLAG },
    { (char *)"curses.wrapper", NULL, &constant_bin[ 1527783 ], 1206, NUITKA_BYTECODE_FLAG },
    { (char *)"dbhash", NULL, &constant_bin[ 1528989 ], 706, NUITKA_BYTECODE_FLAG },
    { (char *)"decimal", NULL, &constant_bin[ 1529695 ], 170849, NUITKA_BYTECODE_FLAG },
    { (char *)"difflib", NULL, &constant_bin[ 1700544 ], 61837, NUITKA_BYTECODE_FLAG },
    { (char *)"dircache", NULL, &constant_bin[ 1762381 ], 1560, NUITKA_BYTECODE_FLAG },
    { (char *)"dis", NULL, &constant_bin[ 1763941 ], 6204, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils", NULL, &constant_bin[ 1770145 ], 405, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"distutils.archive_util", NULL, &constant_bin[ 1770550 ], 7432, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.bcppcompiler", NULL, &constant_bin[ 1777982 ], 7856, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.ccompiler", NULL, &constant_bin[ 1785838 ], 36748, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.cmd", NULL, &constant_bin[ 1822586 ], 16722, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command", NULL, &constant_bin[ 1839308 ], 655, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"distutils.command.bdist", NULL, &constant_bin[ 1839963 ], 5199, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.bdist_dumb", NULL, &constant_bin[ 1845162 ], 5025, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.bdist_msi", NULL, &constant_bin[ 1850187 ], 23971, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.bdist_rpm", NULL, &constant_bin[ 1874158 ], 17724, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.bdist_wininst", NULL, &constant_bin[ 1891882 ], 10950, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.build", NULL, &constant_bin[ 1902832 ], 5125, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.build_clib", NULL, &constant_bin[ 1907957 ], 6405, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.build_ext", NULL, &constant_bin[ 1914362 ], 19417, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.build_py", NULL, &constant_bin[ 1933779 ], 11483, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.build_scripts", NULL, &constant_bin[ 1945262 ], 4512, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.check", NULL, &constant_bin[ 1949774 ], 6236, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.clean", NULL, &constant_bin[ 1956010 ], 3158, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.config", NULL, &constant_bin[ 1959168 ], 12642, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.install", NULL, &constant_bin[ 1971810 ], 17971, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.install_data", NULL, &constant_bin[ 1989781 ], 3142, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.install_egg_info", NULL, &constant_bin[ 1992923 ], 4421, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.install_headers", NULL, &constant_bin[ 1997344 ], 2274, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.install_lib", NULL, &constant_bin[ 1999618 ], 6752, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.install_scripts", NULL, &constant_bin[ 2006370 ], 2976, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.register", NULL, &constant_bin[ 2009346 ], 10188, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.sdist", NULL, &constant_bin[ 2019534 ], 16657, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.upload", NULL, &constant_bin[ 2036191 ], 6308, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.config", NULL, &constant_bin[ 2042499 ], 3548, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.core", NULL, &constant_bin[ 2046047 ], 7634, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.cygwinccompiler", NULL, &constant_bin[ 2053681 ], 9793, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.debug", NULL, &constant_bin[ 2063474 ], 244, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.dep_util", NULL, &constant_bin[ 2063718 ], 3164, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.dir_util", NULL, &constant_bin[ 2066882 ], 6764, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.dist", NULL, &constant_bin[ 2073646 ], 39429, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.emxccompiler", NULL, &constant_bin[ 2113075 ], 7441, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.errors", NULL, &constant_bin[ 2120516 ], 6237, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.extension", NULL, &constant_bin[ 2126753 ], 7396, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.fancy_getopt", NULL, &constant_bin[ 2134149 ], 11908, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.file_util", NULL, &constant_bin[ 2146057 ], 6732, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.filelist", NULL, &constant_bin[ 2152789 ], 10714, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.log", NULL, &constant_bin[ 2163503 ], 2754, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.msvc9compiler", NULL, &constant_bin[ 2166257 ], 21439, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.msvccompiler", NULL, &constant_bin[ 2187696 ], 17465, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.spawn", NULL, &constant_bin[ 2205161 ], 6377, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.sysconfig", NULL, &constant_bin[ 2211538 ], 15100, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.text_file", NULL, &constant_bin[ 2226638 ], 9229, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.unixccompiler", NULL, &constant_bin[ 2235867 ], 8028, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.util", NULL, &constant_bin[ 2243895 ], 14315, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.version", NULL, &constant_bin[ 2258210 ], 7170, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.versionpredicate", NULL, &constant_bin[ 2265380 ], 5520, NUITKA_BYTECODE_FLAG },
    { (char *)"doctest", NULL, &constant_bin[ 2270900 ], 83440, NUITKA_BYTECODE_FLAG },
    { (char *)"dumbdbm", NULL, &constant_bin[ 2354340 ], 6538, NUITKA_BYTECODE_FLAG },
    { (char *)"dummy_thread", NULL, &constant_bin[ 2360878 ], 5356, NUITKA_BYTECODE_FLAG },
    { (char *)"dummy_threading", NULL, &constant_bin[ 2366234 ], 1275, NUITKA_BYTECODE_FLAG },
    { (char *)"email", NULL, &constant_bin[ 2367509 ], 2852, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"email._parseaddr", NULL, &constant_bin[ 2370361 ], 13763, NUITKA_BYTECODE_FLAG },
    { (char *)"email.base64mime", NULL, &constant_bin[ 2384124 ], 5305, NUITKA_BYTECODE_FLAG },
    { (char *)"email.charset", NULL, &constant_bin[ 2389429 ], 13499, NUITKA_BYTECODE_FLAG },
    { (char *)"email.encoders", NULL, &constant_bin[ 2402928 ], 2210, NUITKA_BYTECODE_FLAG },
    { (char *)"email.errors", NULL, &constant_bin[ 2405138 ], 3491, NUITKA_BYTECODE_FLAG },
    { (char *)"email.feedparser", NULL, &constant_bin[ 2408629 ], 11516, NUITKA_BYTECODE_FLAG },
    { (char *)"email.generator", NULL, &constant_bin[ 2420145 ], 10334, NUITKA_BYTECODE_FLAG },
    { (char *)"email.header", NULL, &constant_bin[ 2430479 ], 13622, NUITKA_BYTECODE_FLAG },
    { (char *)"email.iterators", NULL, &constant_bin[ 2444101 ], 2348, NUITKA_BYTECODE_FLAG },
    { (char *)"email.message", NULL, &constant_bin[ 2446449 ], 28576, NUITKA_BYTECODE_FLAG },
    { (char *)"email.mime", NULL, &constant_bin[ 2475025 ], 120, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"email.mime.application", NULL, &constant_bin[ 2475145 ], 1570, NUITKA_BYTECODE_FLAG },
    { (char *)"email.mime.audio", NULL, &constant_bin[ 2476715 ], 2893, NUITKA_BYTECODE_FLAG },
    { (char *)"email.mime.base", NULL, &constant_bin[ 2479608 ], 1102, NUITKA_BYTECODE_FLAG },
    { (char *)"email.mime.image", NULL, &constant_bin[ 2480710 ], 2035, NUITKA_BYTECODE_FLAG },
    { (char *)"email.mime.message", NULL, &constant_bin[ 2482745 ], 1434, NUITKA_BYTECODE_FLAG },
    { (char *)"email.mime.multipart", NULL, &constant_bin[ 2484179 ], 1655, NUITKA_BYTECODE_FLAG },
    { (char *)"email.mime.nonmultipart", NULL, &constant_bin[ 2485834 ], 874, NUITKA_BYTECODE_FLAG },
    { (char *)"email.mime.text", NULL, &constant_bin[ 2486708 ], 1294, NUITKA_BYTECODE_FLAG },
    { (char *)"email.parser", NULL, &constant_bin[ 2488002 ], 3804, NUITKA_BYTECODE_FLAG },
    { (char *)"email.quoprimime", NULL, &constant_bin[ 2491806 ], 8816, NUITKA_BYTECODE_FLAG },
    { (char *)"email.utils", NULL, &constant_bin[ 2500622 ], 9082, NUITKA_BYTECODE_FLAG },
    { (char *)"encodings.mbcs", NULL, &constant_bin[ 2509704 ], 2019, NUITKA_BYTECODE_FLAG },
    { (char *)"filecmp", NULL, &constant_bin[ 2511723 ], 9574, NUITKA_BYTECODE_FLAG },
    { (char *)"fileinput", NULL, &constant_bin[ 2521297 ], 14436, NUITKA_BYTECODE_FLAG },
    { (char *)"fnmatch", NULL, &constant_bin[ 2535733 ], 3594, NUITKA_BYTECODE_FLAG },
    { (char *)"formatter", NULL, &constant_bin[ 2539327 ], 19016, NUITKA_BYTECODE_FLAG },
    { (char *)"fpformat", NULL, &constant_bin[ 2558343 ], 4648, NUITKA_BYTECODE_FLAG },
    { (char *)"fractions", NULL, &constant_bin[ 2562991 ], 19647, NUITKA_BYTECODE_FLAG },
    { (char *)"ftplib", NULL, &constant_bin[ 2582638 ], 34434, NUITKA_BYTECODE_FLAG },
    { (char *)"genericpath", NULL, &constant_bin[ 2617072 ], 3487, NUITKA_BYTECODE_FLAG },
    { (char *)"getopt", NULL, &constant_bin[ 2620559 ], 6626, NUITKA_BYTECODE_FLAG },
    { (char *)"getpass", NULL, &constant_bin[ 2627185 ], 4722, NUITKA_BYTECODE_FLAG },
    { (char *)"gettext", NULL, &constant_bin[ 2631907 ], 15628, NUITKA_BYTECODE_FLAG },
    { (char *)"glob", NULL, &constant_bin[ 2647535 ], 2919, NUITKA_BYTECODE_FLAG },
    { (char *)"gzip", NULL, &constant_bin[ 2650454 ], 15126, NUITKA_BYTECODE_FLAG },
    { (char *)"hashlib", NULL, &constant_bin[ 2665580 ], 7041, NUITKA_BYTECODE_FLAG },
    { (char *)"heapq", NULL, &constant_bin[ 2672621 ], 14520, NUITKA_BYTECODE_FLAG },
    { (char *)"hmac", NULL, &constant_bin[ 2687141 ], 4514, NUITKA_BYTECODE_FLAG },
    { (char *)"hotshot", NULL, &constant_bin[ 2691655 ], 3454, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"hotshot.log", NULL, &constant_bin[ 2695109 ], 5507, NUITKA_BYTECODE_FLAG },
    { (char *)"hotshot.stats", NULL, &constant_bin[ 2700616 ], 3374, NUITKA_BYTECODE_FLAG },
    { (char *)"hotshot.stones", NULL, &constant_bin[ 2703990 ], 1144, NUITKA_BYTECODE_FLAG },
    { (char *)"htmlentitydefs", NULL, &constant_bin[ 2705134 ], 6357, NUITKA_BYTECODE_FLAG },
    { (char *)"htmllib", NULL, &constant_bin[ 2711491 ], 20119, NUITKA_BYTECODE_FLAG },
    { (char *)"httplib", NULL, &constant_bin[ 2731610 ], 36745, NUITKA_BYTECODE_FLAG },
    { (char *)"ihooks", NULL, &constant_bin[ 2768355 ], 21226, NUITKA_BYTECODE_FLAG },
    { (char *)"imaplib", NULL, &constant_bin[ 2789581 ], 45155, NUITKA_BYTECODE_FLAG },
    { (char *)"imghdr", NULL, &constant_bin[ 2834736 ], 4798, NUITKA_BYTECODE_FLAG },
    { (char *)"importlib", NULL, &constant_bin[ 2839534 ], 1488, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"imputil", NULL, &constant_bin[ 2841022 ], 15539, NUITKA_BYTECODE_FLAG },
    { (char *)"inspect", NULL, &constant_bin[ 2856561 ], 39936, NUITKA_BYTECODE_FLAG },
    { (char *)"io", NULL, &constant_bin[ 2896497 ], 3570, NUITKA_BYTECODE_FLAG },
    { (char *)"json", NULL, &constant_bin[ 2900067 ], 13937, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"json.decoder", NULL, &constant_bin[ 2914004 ], 11928, NUITKA_BYTECODE_FLAG },
    { (char *)"json.encoder", NULL, &constant_bin[ 2925932 ], 13668, NUITKA_BYTECODE_FLAG },
    { (char *)"json.scanner", NULL, &constant_bin[ 2939600 ], 2215, NUITKA_BYTECODE_FLAG },
    { (char *)"json.tool", NULL, &constant_bin[ 2941815 ], 1282, NUITKA_BYTECODE_FLAG },
    { (char *)"keyword", NULL, &constant_bin[ 2943097 ], 2093, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3", NULL, &constant_bin[ 2945190 ], 117, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"lib2to3.btm_matcher", NULL, &constant_bin[ 2945307 ], 5800, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.btm_utils", NULL, &constant_bin[ 2951107 ], 7529, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixer_base", NULL, &constant_bin[ 2958636 ], 7216, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixer_util", NULL, &constant_bin[ 2965852 ], 14607, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes", NULL, &constant_bin[ 2980459 ], 123, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"lib2to3.fixes.fix_apply", NULL, &constant_bin[ 2980582 ], 1869, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_asserts", NULL, &constant_bin[ 2982451 ], 1547, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_basestring", NULL, &constant_bin[ 2983998 ], 793, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_buffer", NULL, &constant_bin[ 2984791 ], 950, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_callable", NULL, &constant_bin[ 2985741 ], 1493, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_dict", NULL, &constant_bin[ 2987234 ], 3753, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_except", NULL, &constant_bin[ 2990987 ], 2993, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_exec", NULL, &constant_bin[ 2993980 ], 1418, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_execfile", NULL, &constant_bin[ 2995398 ], 2059, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_exitfunc", NULL, &constant_bin[ 2997457 ], 2739, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_filter", NULL, &constant_bin[ 3000196 ], 2256, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_funcattrs", NULL, &constant_bin[ 3002452 ], 1114, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_future", NULL, &constant_bin[ 3003566 ], 919, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_getcwdu", NULL, &constant_bin[ 3004485 ], 926, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_has_key", NULL, &constant_bin[ 3005411 ], 3184, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_idioms", NULL, &constant_bin[ 3008595 ], 4515, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_import", NULL, &constant_bin[ 3013110 ], 3233, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_imports", NULL, &constant_bin[ 3016343 ], 5352, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_imports2", NULL, &constant_bin[ 3021695 ], 622, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_input", NULL, &constant_bin[ 3022317 ], 1134, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_intern", NULL, &constant_bin[ 3023451 ], 1605, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_isinstance", NULL, &constant_bin[ 3025056 ], 1838, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_itertools", NULL, &constant_bin[ 3026894 ], 1791, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_itertools_imports", NULL, &constant_bin[ 3028685 ], 2016, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_long", NULL, &constant_bin[ 3030701 ], 841, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_map", NULL, &constant_bin[ 3031542 ], 3040, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_metaclass", NULL, &constant_bin[ 3034582 ], 6579, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_methodattrs", NULL, &constant_bin[ 3041161 ], 1138, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_ne", NULL, &constant_bin[ 3042299 ], 985, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_next", NULL, &constant_bin[ 3043284 ], 3531, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_nonzero", NULL, &constant_bin[ 3046815 ], 1086, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_numliterals", NULL, &constant_bin[ 3047901 ], 1249, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_operator", NULL, &constant_bin[ 3049150 ], 5112, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_paren", NULL, &constant_bin[ 3054262 ], 1543, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_print", NULL, &constant_bin[ 3055805 ], 2727, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_raise", NULL, &constant_bin[ 3058532 ], 2498, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_raw_input", NULL, &constant_bin[ 3061030 ], 936, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_reduce", NULL, &constant_bin[ 3061966 ], 1262, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_renames", NULL, &constant_bin[ 3063228 ], 2449, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_repr", NULL, &constant_bin[ 3065677 ], 1016, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_set_literal", NULL, &constant_bin[ 3066693 ], 1988, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_standarderror", NULL, &constant_bin[ 3068681 ], 853, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_sys_exc", NULL, &constant_bin[ 3069534 ], 1705, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_throw", NULL, &constant_bin[ 3071239 ], 1996, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_tuple_params", NULL, &constant_bin[ 3073235 ], 5430, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_types", NULL, &constant_bin[ 3078665 ], 2196, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_unicode", NULL, &constant_bin[ 3080861 ], 1716, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_urllib", NULL, &constant_bin[ 3082577 ], 7134, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_ws_comma", NULL, &constant_bin[ 3089711 ], 1382, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_xrange", NULL, &constant_bin[ 3091093 ], 3063, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_xreadlines", NULL, &constant_bin[ 3094156 ], 1152, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_zip", NULL, &constant_bin[ 3095308 ], 1346, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.main", NULL, &constant_bin[ 3096654 ], 9803, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.patcomp", NULL, &constant_bin[ 3106457 ], 6634, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.pgen2", NULL, &constant_bin[ 3113091 ], 164, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"lib2to3.pgen2.conv", NULL, &constant_bin[ 3113255 ], 8169, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.pgen2.driver", NULL, &constant_bin[ 3121424 ], 5363, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.pgen2.grammar", NULL, &constant_bin[ 3126787 ], 6004, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.pgen2.literals", NULL, &constant_bin[ 3132791 ], 2002, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.pgen2.parse", NULL, &constant_bin[ 3134793 ], 7204, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.pgen2.pgen", NULL, &constant_bin[ 3141997 ], 12101, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.pgen2.token", NULL, &constant_bin[ 3154098 ], 2287, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.pgen2.tokenize", NULL, &constant_bin[ 3156385 ], 16923, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.pygram", NULL, &constant_bin[ 3173308 ], 1400, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.pytree", NULL, &constant_bin[ 3174708 ], 30143, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.refactor", NULL, &constant_bin[ 3204851 ], 23866, NUITKA_BYTECODE_FLAG },
    { (char *)"linecache", NULL, &constant_bin[ 3228717 ], 3252, NUITKA_BYTECODE_FLAG },
    { (char *)"logging", NULL, &constant_bin[ 3231969 ], 57300, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"logging.config", NULL, &constant_bin[ 3289269 ], 25834, NUITKA_BYTECODE_FLAG },
    { (char *)"logging.handlers", NULL, &constant_bin[ 3315103 ], 39156, NUITKA_BYTECODE_FLAG },
    { (char *)"macpath", NULL, &constant_bin[ 3354259 ], 7635, NUITKA_BYTECODE_FLAG },
    { (char *)"macurl2path", NULL, &constant_bin[ 3361894 ], 2228, NUITKA_BYTECODE_FLAG },
    { (char *)"mailbox", NULL, &constant_bin[ 3364122 ], 76279, NUITKA_BYTECODE_FLAG },
    { (char *)"mailcap", NULL, &constant_bin[ 3440401 ], 7045, NUITKA_BYTECODE_FLAG },
    { (char *)"markupbase", NULL, &constant_bin[ 3447446 ], 9281, NUITKA_BYTECODE_FLAG },
    { (char *)"md5", NULL, &constant_bin[ 3456727 ], 368, NUITKA_BYTECODE_FLAG },
    { (char *)"mhlib", NULL, &constant_bin[ 3457095 ], 33653, NUITKA_BYTECODE_FLAG },
    { (char *)"mimetools", NULL, &constant_bin[ 3490748 ], 8176, NUITKA_BYTECODE_FLAG },
    { (char *)"mimetypes", NULL, &constant_bin[ 3498924 ], 18385, NUITKA_BYTECODE_FLAG },
    { (char *)"mimify", NULL, &constant_bin[ 3517309 ], 11963, NUITKA_BYTECODE_FLAG },
    { (char *)"modulefinder", NULL, &constant_bin[ 3529272 ], 19074, NUITKA_BYTECODE_FLAG },
    { (char *)"multifile", NULL, &constant_bin[ 3548346 ], 5382, NUITKA_BYTECODE_FLAG },
    { (char *)"multiprocessing", NULL, &constant_bin[ 3553728 ], 8405, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"multiprocessing.connection", NULL, &constant_bin[ 3562133 ], 14354, NUITKA_BYTECODE_FLAG },
    { (char *)"multiprocessing.dummy", NULL, &constant_bin[ 3576487 ], 5412, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"multiprocessing.dummy.connection", NULL, &constant_bin[ 3581899 ], 2732, NUITKA_BYTECODE_FLAG },
    { (char *)"multiprocessing.forking", NULL, &constant_bin[ 3584631 ], 14336, NUITKA_BYTECODE_FLAG },
    { (char *)"multiprocessing.heap", NULL, &constant_bin[ 3598967 ], 6861, NUITKA_BYTECODE_FLAG },
    { (char *)"multiprocessing.managers", NULL, &constant_bin[ 3605828 ], 38278, NUITKA_BYTECODE_FLAG },
    { (char *)"multiprocessing.pool", NULL, &constant_bin[ 3644106 ], 22367, NUITKA_BYTECODE_FLAG },
    { (char *)"multiprocessing.process", NULL, &constant_bin[ 3666473 ], 9435, NUITKA_BYTECODE_FLAG },
    { (char *)"multiprocessing.queues", NULL, &constant_bin[ 3675908 ], 11466, NUITKA_BYTECODE_FLAG },
    { (char *)"multiprocessing.reduction", NULL, &constant_bin[ 3687374 ], 5972, NUITKA_BYTECODE_FLAG },
    { (char *)"multiprocessing.sharedctypes", NULL, &constant_bin[ 3693346 ], 8572, NUITKA_BYTECODE_FLAG },
    { (char *)"multiprocessing.synchronize", NULL, &constant_bin[ 3701918 ], 10988, NUITKA_BYTECODE_FLAG },
    { (char *)"multiprocessing.util", NULL, &constant_bin[ 3712906 ], 10066, NUITKA_BYTECODE_FLAG },
    { (char *)"mutex", NULL, &constant_bin[ 3722972 ], 2494, NUITKA_BYTECODE_FLAG },
    { (char *)"netrc", NULL, &constant_bin[ 3725466 ], 4646, NUITKA_BYTECODE_FLAG },
    { (char *)"new", NULL, &constant_bin[ 3730112 ], 852, NUITKA_BYTECODE_FLAG },
    { (char *)"nntplib", NULL, &constant_bin[ 3730964 ], 20948, NUITKA_BYTECODE_FLAG },
    { (char *)"ntpath", NULL, &constant_bin[ 3751912 ], 13077, NUITKA_BYTECODE_FLAG },
    { (char *)"nturl2path", NULL, &constant_bin[ 3764989 ], 1801, NUITKA_BYTECODE_FLAG },
    { (char *)"numbers", NULL, &constant_bin[ 3766790 ], 13880, NUITKA_BYTECODE_FLAG },
    { (char *)"opcode", NULL, &constant_bin[ 3780670 ], 6134, NUITKA_BYTECODE_FLAG },
    { (char *)"optparse", NULL, &constant_bin[ 3786804 ], 53841, NUITKA_BYTECODE_FLAG },
    { (char *)"os", NULL, &constant_bin[ 3840645 ], 25585, NUITKA_BYTECODE_FLAG },
    { (char *)"os2emxpath", NULL, &constant_bin[ 3866230 ], 4499, NUITKA_BYTECODE_FLAG },
    { (char *)"pdb", NULL, &constant_bin[ 3870729 ], 43433, NUITKA_BYTECODE_FLAG },
    { (char *)"pickle", NULL, &constant_bin[ 3914162 ], 38342, NUITKA_BYTECODE_FLAG },
    { (char *)"pickletools", NULL, &constant_bin[ 3952504 ], 57072, NUITKA_BYTECODE_FLAG },
    { (char *)"pipes", NULL, &constant_bin[ 4009576 ], 9268, NUITKA_BYTECODE_FLAG },
    { (char *)"pkgutil", NULL, &constant_bin[ 4018844 ], 18859, NUITKA_BYTECODE_FLAG },
    { (char *)"platform", NULL, &constant_bin[ 4037703 ], 37615, NUITKA_BYTECODE_FLAG },
    { (char *)"plistlib", NULL, &constant_bin[ 4075318 ], 19143, NUITKA_BYTECODE_FLAG },
    { (char *)"popen2", NULL, &constant_bin[ 4094461 ], 8985, NUITKA_BYTECODE_FLAG },
    { (char *)"poplib", NULL, &constant_bin[ 4103446 ], 13271, NUITKA_BYTECODE_FLAG },
    { (char *)"posixfile", NULL, &constant_bin[ 4116717 ], 7620, NUITKA_BYTECODE_FLAG },
    { (char *)"posixpath", NULL, &constant_bin[ 4124337 ], 11366, NUITKA_BYTECODE_FLAG },
    { (char *)"pprint", NULL, &constant_bin[ 4135703 ], 10148, NUITKA_BYTECODE_FLAG },
    { (char *)"profile", NULL, &constant_bin[ 4145851 ], 16372, NUITKA_BYTECODE_FLAG },
    { (char *)"pstats", NULL, &constant_bin[ 4162223 ], 24885, NUITKA_BYTECODE_FLAG },
    { (char *)"pty", NULL, &constant_bin[ 4187108 ], 4938, NUITKA_BYTECODE_FLAG },
    { (char *)"py_compile", NULL, &constant_bin[ 4192046 ], 6603, NUITKA_BYTECODE_FLAG },
    { (char *)"pyclbr", NULL, &constant_bin[ 4198649 ], 9617, NUITKA_BYTECODE_FLAG },
    { (char *)"pydoc", NULL, &constant_bin[ 4208266 ], 92288, NUITKA_BYTECODE_FLAG },
    { (char *)"pydoc_data", NULL, &constant_bin[ 4300554 ], 120, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"pydoc_data.topics", NULL, &constant_bin[ 4300674 ], 409111, NUITKA_BYTECODE_FLAG },
    { (char *)"random", NULL, &constant_bin[ 4709785 ], 25476, NUITKA_BYTECODE_FLAG },
    { (char *)"repr", NULL, &constant_bin[ 4735261 ], 5343, NUITKA_BYTECODE_FLAG },
    { (char *)"rexec", NULL, &constant_bin[ 4740604 ], 24068, NUITKA_BYTECODE_FLAG },
    { (char *)"rfc822", NULL, &constant_bin[ 4764672 ], 31721, NUITKA_BYTECODE_FLAG },
    { (char *)"rlcompleter", NULL, &constant_bin[ 4796393 ], 6054, NUITKA_BYTECODE_FLAG },
    { (char *)"robotparser", NULL, &constant_bin[ 4802447 ], 7904, NUITKA_BYTECODE_FLAG },
    { (char *)"runpy", NULL, &constant_bin[ 4810351 ], 8780, NUITKA_BYTECODE_FLAG },
    { (char *)"sched", NULL, &constant_bin[ 4819131 ], 4968, NUITKA_BYTECODE_FLAG },
    { (char *)"sets", NULL, &constant_bin[ 4824099 ], 16775, NUITKA_BYTECODE_FLAG },
    { (char *)"sgmllib", NULL, &constant_bin[ 4840874 ], 15334, NUITKA_BYTECODE_FLAG },
    { (char *)"sha", NULL, &constant_bin[ 4856208 ], 411, NUITKA_BYTECODE_FLAG },
    { (char *)"shelve", NULL, &constant_bin[ 4856619 ], 10194, NUITKA_BYTECODE_FLAG },
    { (char *)"shlex", NULL, &constant_bin[ 4866813 ], 7509, NUITKA_BYTECODE_FLAG },
    { (char *)"shutil", NULL, &constant_bin[ 4874322 ], 18568, NUITKA_BYTECODE_FLAG },
    { (char *)"site", NULL, &constant_bin[ 4892890 ], 17118, NUITKA_BYTECODE_FLAG },
    { (char *)"sitecustomize", NULL, &constant_bin[ 4910008 ], 224, NUITKA_BYTECODE_FLAG },
    { (char *)"smtpd", NULL, &constant_bin[ 4910232 ], 15826, NUITKA_BYTECODE_FLAG },
    { (char *)"smtplib", NULL, &constant_bin[ 4926058 ], 30169, NUITKA_BYTECODE_FLAG },
    { (char *)"sndhdr", NULL, &constant_bin[ 4956227 ], 7315, NUITKA_BYTECODE_FLAG },
    { (char *)"socket", NULL, &constant_bin[ 4963542 ], 16084, NUITKA_BYTECODE_FLAG },
    { (char *)"sqlite3", NULL, &constant_bin[ 4979626 ], 154, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"sqlite3.dbapi2", NULL, &constant_bin[ 4979780 ], 2682, NUITKA_BYTECODE_FLAG },
    { (char *)"sqlite3.dump", NULL, &constant_bin[ 4982462 ], 2057, NUITKA_BYTECODE_FLAG },
    { (char *)"sre", NULL, &constant_bin[ 4984519 ], 509, NUITKA_BYTECODE_FLAG },
    { (char *)"ssl", NULL, &constant_bin[ 4985028 ], 32205, NUITKA_BYTECODE_FLAG },
    { (char *)"stat", NULL, &constant_bin[ 5017233 ], 2723, NUITKA_BYTECODE_FLAG },
    { (char *)"statvfs", NULL, &constant_bin[ 5019956 ], 610, NUITKA_BYTECODE_FLAG },
    { (char *)"stringold", NULL, &constant_bin[ 5020566 ], 12487, NUITKA_BYTECODE_FLAG },
    { (char *)"subprocess", NULL, &constant_bin[ 5033053 ], 42004, NUITKA_BYTECODE_FLAG },
    { (char *)"sunau", NULL, &constant_bin[ 5075057 ], 18301, NUITKA_BYTECODE_FLAG },
    { (char *)"sunaudio", NULL, &constant_bin[ 5093358 ], 1969, NUITKA_BYTECODE_FLAG },
    { (char *)"symbol", NULL, &constant_bin[ 5095327 ], 3014, NUITKA_BYTECODE_FLAG },
    { (char *)"symtable", NULL, &constant_bin[ 5098341 ], 11678, NUITKA_BYTECODE_FLAG },
    { (char *)"sysconfig", NULL, &constant_bin[ 5110019 ], 18670, NUITKA_BYTECODE_FLAG },
    { (char *)"tabnanny", NULL, &constant_bin[ 5128689 ], 8195, NUITKA_BYTECODE_FLAG },
    { (char *)"tarfile", NULL, &constant_bin[ 5136884 ], 75638, NUITKA_BYTECODE_FLAG },
    { (char *)"telnetlib", NULL, &constant_bin[ 5212522 ], 23073, NUITKA_BYTECODE_FLAG },
    { (char *)"tempfile", NULL, &constant_bin[ 5235595 ], 20241, NUITKA_BYTECODE_FLAG },
    { (char *)"test", NULL, &constant_bin[ 5255836 ], 114, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"test.pystone", NULL, &constant_bin[ 5255950 ], 7981, NUITKA_BYTECODE_FLAG },
    { (char *)"textwrap", NULL, &constant_bin[ 5263931 ], 11996, NUITKA_BYTECODE_FLAG },
    { (char *)"this", NULL, &constant_bin[ 5275927 ], 1210, NUITKA_BYTECODE_FLAG },
    { (char *)"threading", NULL, &constant_bin[ 5277137 ], 42422, NUITKA_BYTECODE_FLAG },
    { (char *)"timeit", NULL, &constant_bin[ 5319559 ], 12149, NUITKA_BYTECODE_FLAG },
    { (char *)"tkColorChooser", NULL, &constant_bin[ 5331708 ], 1402, NUITKA_BYTECODE_FLAG },
    { (char *)"tkCommonDialog", NULL, &constant_bin[ 5333110 ], 1496, NUITKA_BYTECODE_FLAG },
    { (char *)"tkFileDialog", NULL, &constant_bin[ 5334606 ], 5121, NUITKA_BYTECODE_FLAG },
    { (char *)"tkFont", NULL, &constant_bin[ 5339727 ], 7076, NUITKA_BYTECODE_FLAG },
    { (char *)"tkMessageBox", NULL, &constant_bin[ 5346803 ], 3864, NUITKA_BYTECODE_FLAG },
    { (char *)"tkSimpleDialog", NULL, &constant_bin[ 5350667 ], 9053, NUITKA_BYTECODE_FLAG },
    { (char *)"toaiff", NULL, &constant_bin[ 5359720 ], 3090, NUITKA_BYTECODE_FLAG },
    { (char *)"token", NULL, &constant_bin[ 5362810 ], 3798, NUITKA_BYTECODE_FLAG },
    { (char *)"tokenize", NULL, &constant_bin[ 5366608 ], 14465, NUITKA_BYTECODE_FLAG },
    { (char *)"trace", NULL, &constant_bin[ 5381073 ], 22716, NUITKA_BYTECODE_FLAG },
    { (char *)"traceback", NULL, &constant_bin[ 5403789 ], 11631, NUITKA_BYTECODE_FLAG },
    { (char *)"ttk", NULL, &constant_bin[ 5415420 ], 62193, NUITKA_BYTECODE_FLAG },
    { (char *)"tty", NULL, &constant_bin[ 5477613 ], 1303, NUITKA_BYTECODE_FLAG },
    { (char *)"turtle", NULL, &constant_bin[ 5478916 ], 139008, NUITKA_BYTECODE_FLAG },
    { (char *)"unittest", NULL, &constant_bin[ 5617924 ], 2954, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"unittest.case", NULL, &constant_bin[ 5620878 ], 40172, NUITKA_BYTECODE_FLAG },
    { (char *)"unittest.loader", NULL, &constant_bin[ 5661050 ], 11299, NUITKA_BYTECODE_FLAG },
    { (char *)"unittest.main", NULL, &constant_bin[ 5672349 ], 7985, NUITKA_BYTECODE_FLAG },
    { (char *)"unittest.result", NULL, &constant_bin[ 5680334 ], 7869, NUITKA_BYTECODE_FLAG },
    { (char *)"unittest.runner", NULL, &constant_bin[ 5688203 ], 7599, NUITKA_BYTECODE_FLAG },
    { (char *)"unittest.signals", NULL, &constant_bin[ 5695802 ], 2738, NUITKA_BYTECODE_FLAG },
    { (char *)"unittest.suite", NULL, &constant_bin[ 5698540 ], 10460, NUITKA_BYTECODE_FLAG },
    { (char *)"unittest.util", NULL, &constant_bin[ 5709000 ], 4494, NUITKA_BYTECODE_FLAG },
    { (char *)"urllib", NULL, &constant_bin[ 5713494 ], 50856, NUITKA_BYTECODE_FLAG },
    { (char *)"urllib2", NULL, &constant_bin[ 5764350 ], 47064, NUITKA_BYTECODE_FLAG },
    { (char *)"urlparse", NULL, &constant_bin[ 5811414 ], 14457, NUITKA_BYTECODE_FLAG },
    { (char *)"user", NULL, &constant_bin[ 5825871 ], 1714, NUITKA_BYTECODE_FLAG },
    { (char *)"uu", NULL, &constant_bin[ 5827585 ], 4294, NUITKA_BYTECODE_FLAG },
    { (char *)"uuid", NULL, &constant_bin[ 5831879 ], 22469, NUITKA_BYTECODE_FLAG },
    { (char *)"warnings", NULL, &constant_bin[ 5854348 ], 13570, NUITKA_BYTECODE_FLAG },
    { (char *)"wave", NULL, &constant_bin[ 5867918 ], 19911, NUITKA_BYTECODE_FLAG },
    { (char *)"weakref", NULL, &constant_bin[ 5887829 ], 15733, NUITKA_BYTECODE_FLAG },
    { (char *)"webbrowser", NULL, &constant_bin[ 5903562 ], 19681, NUITKA_BYTECODE_FLAG },
    { (char *)"whichdb", NULL, &constant_bin[ 5923243 ], 2229, NUITKA_BYTECODE_FLAG },
    { (char *)"wsgiref", NULL, &constant_bin[ 5925472 ], 719, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"wsgiref.handlers", NULL, &constant_bin[ 5926191 ], 16172, NUITKA_BYTECODE_FLAG },
    { (char *)"wsgiref.headers", NULL, &constant_bin[ 5942363 ], 7429, NUITKA_BYTECODE_FLAG },
    { (char *)"wsgiref.simple_server", NULL, &constant_bin[ 5949792 ], 6199, NUITKA_BYTECODE_FLAG },
    { (char *)"wsgiref.util", NULL, &constant_bin[ 5955991 ], 5961, NUITKA_BYTECODE_FLAG },
    { (char *)"wsgiref.validate", NULL, &constant_bin[ 5961952 ], 16786, NUITKA_BYTECODE_FLAG },
    { (char *)"xdrlib", NULL, &constant_bin[ 5978738 ], 9810, NUITKA_BYTECODE_FLAG },
    { (char *)"xml", NULL, &constant_bin[ 5988548 ], 1068, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"xml.dom", NULL, &constant_bin[ 5989616 ], 6427, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"xml.dom.NodeFilter", NULL, &constant_bin[ 5996043 ], 1112, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.dom.domreg", NULL, &constant_bin[ 5997155 ], 3293, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.dom.expatbuilder", NULL, &constant_bin[ 6000448 ], 33003, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.dom.minicompat", NULL, &constant_bin[ 6033451 ], 3394, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.dom.minidom", NULL, &constant_bin[ 6036845 ], 65227, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.dom.pulldom", NULL, &constant_bin[ 6102072 ], 12986, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.dom.xmlbuilder", NULL, &constant_bin[ 6115058 ], 16396, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.etree", NULL, &constant_bin[ 6131454 ], 119, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"xml.etree.ElementInclude", NULL, &constant_bin[ 6131573 ], 1951, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.etree.ElementPath", NULL, &constant_bin[ 6133524 ], 7560, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.etree.ElementTree", NULL, &constant_bin[ 6141084 ], 34794, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.etree.cElementTree", NULL, &constant_bin[ 6175878 ], 166, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.parsers", NULL, &constant_bin[ 6176044 ], 304, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"xml.parsers.expat", NULL, &constant_bin[ 6176348 ], 277, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.sax", NULL, &constant_bin[ 6176625 ], 3679, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"xml.sax._exceptions", NULL, &constant_bin[ 6180304 ], 6127, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.sax.expatreader", NULL, &constant_bin[ 6186431 ], 14701, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.sax.handler", NULL, &constant_bin[ 6201132 ], 12970, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.sax.saxutils", NULL, &constant_bin[ 6214102 ], 14685, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.sax.xmlreader", NULL, &constant_bin[ 6228787 ], 19078, NUITKA_BYTECODE_FLAG },
    { (char *)"xmllib", NULL, &constant_bin[ 6247865 ], 26732, NUITKA_BYTECODE_FLAG },
    { (char *)"xmlrpclib", NULL, &constant_bin[ 6274597 ], 43832, NUITKA_BYTECODE_FLAG },
    { (char *)"zipfile", NULL, &constant_bin[ 6318429 ], 41594, NUITKA_BYTECODE_FLAG },
    { NULL, NULL, 0 }
};

void setupMetaPathBasedLoader( void )
{
    static bool init_done = false;

    if ( init_done == false )
    {
        registerMetaPathBasedUnfreezer( meta_path_loader_entries );
        init_done = true;
    }
}
